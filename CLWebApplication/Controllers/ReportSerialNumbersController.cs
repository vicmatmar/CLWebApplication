using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CLWebApp.Controllers
{
    public class ReportSerialNumbersController : Controller
    {
        private readonly ManufacturingStore_Context _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        static List<FinalTestCount> _finalTestCounts = new List<FinalTestCount>();

        public ReportSerialNumbersController(ManufacturingStore_Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductionSite.ToListAsync());
        }

        public async Task<IActionResult> Report(int siteid, int number_of_days, int time_offset_hrs)
        {
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
                string msg = $"Nothing found for site = {siteid}, days = {number_of_days}";
                return Content(msg);
            }

            var list = await q1.ToListAsync();

            _finalTestCounts = new List<FinalTestCount>();
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
                _finalTestCounts.Add(finalTestCount);
            }

            //ViewData["Title"] = $"site = {siteid}, days = {number_of_days}";

            return View(_finalTestCounts);
        }

        public async Task<IActionResult> DownloadFile()
        {
            if (_finalTestCounts == null || _finalTestCounts.Count == 0)
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
                foreach (FinalTestCount finalTestCount in _finalTestCounts)
                {
                    string line = $"{finalTestCount.Header.TotalCount},{finalTestCount.Header.Sku},\"{finalTestCount.Header.ProductName}\"";
                    streamWriter.WriteLine(line);
                    foreach(FinalTestCountItem item in finalTestCount.Items)
                    {
                        line = $"{item.TotalCount},{item.CreatedDate.ToShortDateString()}";
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