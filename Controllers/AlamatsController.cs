#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce.Datas;
using e_commerce.Datas.Entities;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce.Controllers
{
    public class AlamatsController : Controller
    {
        private readonly ecommerceContext _context;

        public AlamatsController(ecommerceContext context)
        {
            _context = context;
        }

        // GET: Alamats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alamats.ToListAsync());
        }

        // GET: Alamats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alamat = await _context.Alamats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alamat == null)
            {
                return NotFound();
            }

            return View(alamat);
        }

        // GET: Alamats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alamats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kecamatan,Kelurahan,Rt,Rw,KodePos,Detail")] Alamat alamat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alamat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alamat);
        }

        // GET: Alamats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alamat = await _context.Alamats.FindAsync(id);
            if (alamat == null)
            {
                return NotFound();
            }
            return View(alamat);
        }

        // POST: Alamats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kecamatan,Kelurahan,Rt,Rw,KodePos,Detail")] Alamat alamat)
        {
            if (id != alamat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alamat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlamatExists(alamat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alamat);
        }

        // GET: Alamats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alamat = await _context.Alamats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alamat == null)
            {
                return NotFound();
            }

            return View(alamat);
        }

        // POST: Alamats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alamat = await _context.Alamats.FindAsync(id);
            _context.Alamats.Remove(alamat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlamatExists(int id)
        {
            return _context.Alamats.Any(e => e.Id == id);
        }
    }
}
