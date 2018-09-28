using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CLWebApp.Controllers
{
    public class ReportSerialNumbersController : Controller
    {
        private readonly ManufacturingStore_Context _context;

        public ReportSerialNumbersController(ManufacturingStore_Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductionSite.ToListAsync());
        }

        public async Task<IActionResult> Report(int siteid, int number_of_days, TimeSpan time_offset_hrs)
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

            string txt = "";
            List<FinalTestCount> finalTestCounts = new List<FinalTestCount>();
            foreach (var k1 in list)
            {
                FinalTestCount finalTestCount = new FinalTestCount();
                finalTestCount.Header = new FinalTestCountHeader { TotalCount = k1.Count(), Sku = k1.Key, ProductName = k1.First().ProductName };

                string r = $"{k1.Count()}\t{k1.Key}\t{k1.First().ProductName}";
                txt += $"{r}<br>";

                List<ReportSerialNumber> datelist = new List<ReportSerialNumber>();
                foreach (ReportSerialNumber e in k1)
                {
                    e.CreateDate += time_offset_hrs;
                    e.CreateDate = new DateTime(e.CreateDate.Year, e.CreateDate.Month, e.CreateDate.Day);
                    datelist.Add(e);
                }

                var dgs = datelist.GroupBy(d => d.CreateDate);
                List<FinalTestCountItem> finalTestCountItems = new List<FinalTestCountItem>();
                foreach (var dg in dgs)
                {
                    FinalTestCountItem testCountItem = new FinalTestCountItem { TotalCount = dg.Count(), CreatedDate = dg.Key.Date };
                    finalTestCountItems.Add(testCountItem);

                    r = $"\t{dg.Count()}\t{dg.Key.Date.ToShortDateString()}";
                    txt += $"{r}<br>";
                }

                finalTestCount.Items = finalTestCountItems.ToArray();
                finalTestCounts.Add(finalTestCount);
            }

            //ViewData["Title"] = $"site = {siteid}, days = {number_of_days}";
            ViewData["Message"] = txt;

            return View(finalTestCounts);
        }
    }
}