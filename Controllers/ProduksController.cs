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
    public class ProduksController : Controller
    {
        private readonly IProdukService _produkService;
        private readonly IKategoriService _kategoriService;
        private readonly ecommerceContext _context;

        public ProduksController(ecommerceContext context, IProdukService produkService, IKategoriService kategoriService)
        {
            _context = context;
            _produkService = produkService;
            _kategoriService = kategoriService;
        }

        // GET: Produks
        public async Task<IActionResult> Index()
        {
            var dbResult = await _produkService.GetAll();
            var viewModel = new List<ProdukViewModel>();

            for (int i = 0; i < dbResult.Count; i++)
            {
                viewModel.Add(new ProdukViewModel
                {
                    Id = dbResult[i].Id,
                    Harga = dbResult[i].Harga,
                    Nama = dbResult[i].Nama,
                    Deskripsi = dbResult[i].Deskripsi,
                    Stock = dbResult[i].Stock,
                    Gambar = dbResult[i].Gambar,
                });
            }

            return View(viewModel);
        }

        // GET: Produks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var dataProduk = await _produkService.Get(id);
            var dataViewModel = new ProdukViewModel
            {
                Id = dataProduk.Id,
                Harga = dataProduk.Harga,
                Nama = dataProduk.Nama,
                Gambar = dataProduk.Gambar,
                Deskripsi = dataProduk.Deskripsi,
                Stock = dataProduk.Stock
            };
            return View(dataViewModel);
        }

        private async Task SetKategoriDataSource()
        {
            var kategoriViewModels = await _kategoriService.GetAll();

            ViewBag.KategoriDataSource = kategoriViewModels.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nama,
                Selected = false
            }).ToList();
        }



        // GET: Produks/Create
        public async Task<IActionResult> Create()
        {
            await SetKategoriDataSource();
            return View(new ProdukViewModel());
        }

        // POST: Produks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdukViewModel dataInput)
        {
            if (!ModelState.IsValid)
            {
                return View(dataInput);
            }
            try
            {
                var dataproduk = dataInput.ConvertToDbModel();
                dataproduk.KategoriProduks.Add(new Datas.Entities.KategoriProduk
                {
                    IdKategori = dataInput.KategoriId,
                    IdProduk = dataproduk.Id,
                });

                await _produkService.Add(dataproduk);

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

            return View(dataInput);
        }

        // GET: Produks/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var datainput = await _produkService.Get(id);

            return View(datainput);
        }

        // POST: Produks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Produk dataProduk)
        {
            var dataKategori = await _produkService.Update(dataProduk);
            return RedirectToAction(nameof(Index));
        }

        // GET: Produks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _context.Produks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produk == null)
            {
                return NotFound();
            }

            return View(produk);
        }

        // POST: Produks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _produkService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
