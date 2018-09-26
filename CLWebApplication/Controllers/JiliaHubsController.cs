using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CLWebApp.Models;
using Microsoft.AspNetCore.Http;

using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CLWebApp.Controllers
{
    public class JiliaHubsController : Controller
    {
        private readonly ManufacturingStore_Context _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public JiliaHubsController(ManufacturingStore_Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> DownloadFile()
        {

            var jiliaHubs = _context.JiliaHubs.OrderByDescending(j=>j.Timestamp);
            if (jiliaHubs == null)
            {
                return NotFound();
            }

            string webRootPath = _hostingEnvironment.WebRootPath;
            string filename = "JiliaHubs.csv";
            string loc = Path.Combine(webRootPath, filename);

            if (System.IO.File.Exists(loc))
            {
                System.IO.File.Delete(loc);
            }

            var props = typeof(JiliaHubs).GetProperties();
            using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(loc))
            {
                foreach(var hub in jiliaHubs)
                {
                    string line = "";
                    foreach (var prop in props)
                    {
                        var val = prop.GetValue(hub);
                        if (val.GetType() == typeof(string))
                            val = ((string)val).Trim();
                        line += val + ",";
                    }
                    line = line.TrimEnd(',');
                    streamWriter.WriteLine(line);
                }
            }


            FileUpDownController fileUp = new FileUpDownController();
            return await fileUp.Download(filename);

        }
        // GET: JiliaHubs
        public async Task<IActionResult> Index()
        {
            return View(await _context.JiliaHubs.OrderByDescending(j=>j.Timestamp).ToListAsync());
        }

        // GET: JiliaHubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jiliaHubs = await _context.JiliaHubs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jiliaHubs == null)
            {
                return NotFound();
            }

            return View(jiliaHubs);
        }

        //// GET: JiliaHubs/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: JiliaHubs/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Bid,Mac,Uid,Activation,Timestamp")] JiliaHubs jiliaHubs)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(jiliaHubs);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(jiliaHubs);
        //}

        // GET: JiliaHubs/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var jiliaHubs = await _context.JiliaHubs.FindAsync(id);
        //    if (jiliaHubs == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(jiliaHubs);
        //}

        //// POST: JiliaHubs/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Bid,Mac,Uid,Activation,Timestamp")] JiliaHubs jiliaHubs)
        //{
        //    if (id != jiliaHubs.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(jiliaHubs);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!JiliaHubsExists(jiliaHubs.Id))
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
        //    return View(jiliaHubs);
        //}

        //// GET: JiliaHubs/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var jiliaHubs = await _context.JiliaHubs
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (jiliaHubs == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(jiliaHubs);
        //}

        //// POST: JiliaHubs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var jiliaHubs = await _context.JiliaHubs.FindAsync(id);
        //    _context.JiliaHubs.Remove(jiliaHubs);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool JiliaHubsExists(int id)
        {
            return _context.JiliaHubs.Any(e => e.Id == id);
        }
    }
}
