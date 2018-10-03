using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CLWebApp.Controllers
{
    public class ReportSerialNumbersController : Controller
    {
        private readonly ManufacturingStore_Context _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        static FinalTestReport _finalTestReport;

        //const string SiteCookyName = ""

        public ReportSerialNumbersController(ManufacturingStore_Context context,
            IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> ReportPostAsync(int Siteid, int NumberOfDays, int HrOffset)
        {
            return await Report(Siteid, NumberOfDays, HrOffset);
        }

        public async Task<IActionResult> ReportFromCookiesAsync()
        {
            string value = Request.Cookies["ReportSiteId"];
            if (string.IsNullOrEmpty(value))
                value = "2";
            int siteid = Convert.ToInt32(value);

            value = Request.Cookies["ReportNumberOfDays"];
            if (string.IsNullOrEmpty(value))
                value = "10";
            int number_of_days = Convert.ToInt32(value);

            value = Request.Cookies["ReportTimeOffset"];
            if (string.IsNullOrEmpty(value))
                value = "0";
            int time_offset_hrs = Convert.ToInt32(value);

            return await Report(siteid, number_of_days, time_offset_hrs);
        }

        public async Task<IActionResult> Report(int siteid, int number_of_days, int time_offset_hrs)
        {
            _finalTestReport = new FinalTestReport(siteid, number_of_days, time_offset_hrs);
            _finalTestReport.FinalTestCounts = await getFinalTestCountsAsync(siteid, number_of_days, time_offset_hrs);
            if (_finalTestReport?.FinalTestCounts?.Length == 0)
            {
                string msg = $"Nothing found for site = {siteid}, days = {number_of_days}";
                return Content(msg);
            }

            return View("Report", _finalTestReport);
        }

        void setCookies(int siteid, int number_of_days, int time_offset_hrs)
        {
            CookieOptions cookieOptions = new CookieOptions { Expires = DateTime.Now.AddDays(5) };

            Response.Cookies.Append("ReportSiteId", siteid.ToString(), cookieOptions);
            Response.Cookies.Append("ReportNumberOfDays", number_of_days.ToString(), cookieOptions);
            Response.Cookies.Append("ReportTimeOffset", time_offset_hrs.ToString(), cookieOptions);
        }

        async Task<FinalTestCount[]> getFinalTestCountsAsync(int siteid, int number_of_days, int time_offset_hrs)
        {
            setCookies(siteid, number_of_days, time_offset_hrs);

            DateTime fromDate = DateTime.Now - new TimeSpan(number_of_days, 0, 0, 0);

            var q1 = _context.SerialNumber
                .Where(s => s.Eui.ProductionSiteId == siteid && s.CreateDate >= fromDate)
                .Select(s => new ReportSerialNumber
                {
                    Eui = s.Eui.Eui,
                    Sku = s.Product.Sku,
                    ProductName = s.Product.Name,
                    CreateDate = s.CreateDate,
                    SerialNumber = s.SerialNumber1
                })
                .OrderByDescending(s => s.CreateDate)
                .GroupBy(s => s.Sku);

            if (!await q1.AnyAsync())
            {
                return null;
            }

            var list = await q1.ToListAsync();
            List<FinalTestCount> finalTestCounts = new List<FinalTestCount>();
            foreach (var k1 in list)
            {
                FinalTestCount finalTestCount = new FinalTestCount();
                finalTestCount.Header = new FinalTestCountHeader { TotalCount = k1.Count(), Sku = k1.Key, ProductName = k1.First().ProductName };

                List<ReportSerialNumber> datelist = new List<ReportSerialNumber>();
                foreach (ReportSerialNumber e in k1)
                {
                    e.CreateDate += new TimeSpan(time_offset_hrs, 0, 0);
                    e.CreateDate = new DateTime(e.CreateDate.Year, e.CreateDate.Month, e.CreateDate.Day);
                    datelist.Add(e);
                }

                var dgs = datelist.GroupBy(d => d.CreateDate);
                List<FinalTestCountItem> finalTestCountItems = new List<FinalTestCountItem>();
                foreach (var dg in dgs)
                {
                    FinalTestCountItem testCountItem = new FinalTestCountItem { TotalCount = dg.Count(), CreatedDate = dg.Key.Date };
                    finalTestCountItems.Add(testCountItem);

                }

                finalTestCount.Items = finalTestCountItems.ToArray();
                finalTestCounts.Add(finalTestCount);
            }

            return finalTestCounts.ToArray();

        }
        public async Task<IActionResult> DownloadFile()
        {
            if (_finalTestReport?.FinalTestCounts?.Length == 0)
                return Content("No data found");

            string webRootPath = _hostingEnvironment.WebRootPath;
            string filename = "FinalTestReport.csv";
            string loc = Path.Combine(webRootPath, filename);

            if (System.IO.File.Exists(loc))
            {
                System.IO.File.Delete(loc);
            }

            using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(loc))
            {
                foreach (FinalTestCount finalTestCount in _finalTestReport.FinalTestCounts)
                {
                    string line = $"{finalTestCount.Header.Sku},\"{finalTestCount.Header.ProductName}\",{finalTestCount.Header.TotalCount}";
                    streamWriter.WriteLine(line);
                    foreach (FinalTestCountItem item in finalTestCount.Items)
                    {
                        line = $",{item.CreatedDate.ToShortDateString()},{item.TotalCount}";
                        streamWriter.WriteLine(line);

                    }
                    streamWriter.WriteLine("-----,-----,-----");
                }
            }

            FileUpDownController fileUp = new FileUpDownController();
            return await fileUp.Download(filename);

        }

    }
}