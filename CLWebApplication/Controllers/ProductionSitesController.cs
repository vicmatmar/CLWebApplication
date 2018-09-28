using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CLWebApp.Models;

namespace CLWebApp.Controllers
{
    public class ProductionSitesController : Controller
    {
        private readonly ManufacturingStore_Context _context;

        public ProductionSitesController(ManufacturingStore_Context context)
        {
            _context = context;
        }

        // GET: ProductionSites
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductionSite.ToListAsync());
        }

        // GET: ProductionSites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionSite = await _context.ProductionSite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productionSite == null)
            {
                return NotFound();
            }

            return View(productionSite);
        }

        //// GET: ProductionSites/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ProductionSites/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,LoadRangeTest,RunIct,RunRangeTest,LoadApplication,ForceChannel,Erase,EnableFirmwareChange")] ProductionSite productionSite)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(productionSite);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(productionSite);
        //}

        //// GET: ProductionSites/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var productionSite = await _context.ProductionSite.FindAsync(id);
        //    if (productionSite == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productionSite);
        //}

        //// POST: ProductionSites/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LoadRangeTest,RunIct,RunRangeTest,LoadApplication,ForceChannel,Erase,EnableFirmwareChange")] ProductionSite productionSite)
        //{
        //    if (id != productionSite.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(productionSite);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductionSiteExists(productionSite.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(productionSite);
        //}

        //// GET: ProductionSites/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var productionSite = await _context.ProductionSite
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (productionSite == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productionSite);
        //}

        //// POST: ProductionSites/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var productionSite = await _context.ProductionSite.FindAsync(id);
        //    _context.ProductionSite.Remove(productionSite);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ProductionSiteExists(int id)
        {
            return _context.ProductionSite.Any(e => e.Id == id);
        }
    }
}
