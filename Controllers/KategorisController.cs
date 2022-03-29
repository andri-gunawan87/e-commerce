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
using e_commerce.Interface;
using e_commerce.ViewModels;

namespace e_commerce.Controllers
{
    public class KategorisController : Controller
    {
        private readonly IKategoriService _kategoriService;
        private readonly ecommerceContext _context;

        public KategorisController(ecommerceContext context, IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
            _context = context;
        }

        // GET: Kategoris
        public async Task<IActionResult> Index()
        {
            var dbResult = await _kategoriService.GetAll();

            var viewModels = new List<KategoriViewModel>();

            for (int i = 0; i < dbResult.Count; i++)
            {
                viewModels.Add(new KategoriViewModel
                {
                    Id = dbResult[i].Id,
                    Nama = dbResult[i].Nama,
                    Deskripsi = dbResult[i].Deskripsi,
                    Icon = dbResult[i].Icon,
                });
            }

            return View(viewModels);
        }

        // GET: Kategoris/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var dataKategori = await _kategoriService.Get(id);
            var dataViewModel = new KategoriViewModel
            {
                Id = dataKategori.Id,
                Nama = dataKategori.Nama,
                Deskripsi = dataKategori.Deskripsi,
                Icon = dataKategori.Icon
            };
            return View(dataViewModel);
        }

        // GET: Kategoris/Create
        public IActionResult Create()
        {
            return View(new KategoriViewModel());
        }

        // POST: Kategoris/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KategoriViewModel dataKategori)
        {
            if (!ModelState.IsValid)
            {
                return View(dataKategori);
            }
            try
            {
                await _kategoriService.Add(dataKategori.ConvertToDbModel());

                return Redirect(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                throw;
            }

            return View(dataKategori);
        }

        // GET: Kategoris/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var kategori = await _context.Kategoris.FindAsync(id);
            //if (kategori == null)
            //{
            //    return NotFound();
            //}
            //return View(kategori);
            var datainput = await _kategoriService.Get(id);
            
            return View(datainput);
        }

        // POST: Kategoris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Kategori kategori)
        {
            var dataKategori = await _kategoriService.Update(kategori);
            return RedirectToAction(nameof(Index));
        }

        // GET: Kategoris/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var dataKategori = await _kategoriService.Get(id);
            return View(dataKategori);
        }

        // POST: Kategoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _kategoriService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
