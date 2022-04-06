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

namespace e_commerce.Controllers
{
    public class KategoriProduksController : BaseController
    {
        private readonly ecommerceContext _context;

        public KategoriProduksController(ecommerceContext context)
        {
            _context = context;
        }

        // GET: KategoriProduks
        public async Task<IActionResult> Index()
        {
            var ecommerceContext = _context.KategoriProduks.Include(k => k.IdKategoriNavigation).Include(k => k.IdProdukNavigation);
            return View(await ecommerceContext.ToListAsync());
        }

        // GET: KategoriProduks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoriProduk = await _context.KategoriProduks
                .Include(k => k.IdKategoriNavigation)
                .Include(k => k.IdProdukNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategoriProduk == null)
            {
                return NotFound();
            }

            return View(kategoriProduk);
        }

        // GET: KategoriProduks/Create
        public IActionResult Create()
        {
            ViewData["IdKategori"] = new SelectList(_context.Kategoris, "Id", "Id");
            ViewData["IdProduk"] = new SelectList(_context.Produks, "Id", "Id");
            return View();
        }

        // POST: KategoriProduks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdProduk,IdKategori")] KategoriProduk kategoriProduk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategoriProduk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKategori"] = new SelectList(_context.Kategoris, "Id", "Id", kategoriProduk.IdKategori);
            ViewData["IdProduk"] = new SelectList(_context.Produks, "Id", "Id", kategoriProduk.IdProduk);
            return View(kategoriProduk);
        }

        // GET: KategoriProduks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoriProduk = await _context.KategoriProduks.FindAsync(id);
            if (kategoriProduk == null)
            {
                return NotFound();
            }
            ViewData["IdKategori"] = new SelectList(_context.Kategoris, "Id", "Id", kategoriProduk.IdKategori);
            ViewData["IdProduk"] = new SelectList(_context.Produks, "Id", "Id", kategoriProduk.IdProduk);
            return View(kategoriProduk);
        }

        // POST: KategoriProduks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdProduk,IdKategori")] KategoriProduk kategoriProduk)
        {
            if (id != kategoriProduk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategoriProduk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriProdukExists(kategoriProduk.Id))
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
            ViewData["IdKategori"] = new SelectList(_context.Kategoris, "Id", "Id", kategoriProduk.IdKategori);
            ViewData["IdProduk"] = new SelectList(_context.Produks, "Id", "Id", kategoriProduk.IdProduk);
            return View(kategoriProduk);
        }

        // GET: KategoriProduks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoriProduk = await _context.KategoriProduks
                .Include(k => k.IdKategoriNavigation)
                .Include(k => k.IdProdukNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategoriProduk == null)
            {
                return NotFound();
            }

            return View(kategoriProduk);
        }

        // POST: KategoriProduks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategoriProduk = await _context.KategoriProduks.FindAsync(id);
            _context.KategoriProduks.Remove(kategoriProduk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriProdukExists(int id)
        {
            return _context.KategoriProduks.Any(e => e.Id == id);
        }
    }
}
