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
using e_commerce.Helpers;

namespace e_commerce.Controllers
{
    public class ProduksController : Controller
    {
        private readonly IProdukService _produkService;
        private readonly IKategoriService _kategoriService;
        private readonly IProdukKategoriService _produkKategoriService;
        private readonly IWebHostEnvironment _iwebHost;
        private readonly ecommerceContext _context;

        public ProduksController(ecommerceContext context,
            IProdukService produkService,
            IKategoriService kategoriService,
            IProdukKategoriService produkKategoriService,
            IWebHostEnvironment iwebHost)
        {
            _context = context;
            _produkService = produkService;
            _kategoriService = kategoriService;
            _produkKategoriService = produkKategoriService;
            _iwebHost = iwebHost;
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
                    Kategories = dbResult[i].KategoriProduks.Select(x => new KategoriViewModel
                    {
                        Id = x.IdKategori,
                        Nama = x.IdKategoriNavigation.Nama,
                        Icon = x.IdKategoriNavigation.Icon
                    }).ToList()
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
                //Selected = false
            }).ToList();
        }

        private async Task SetKategoriDataSource(int[] kategoris)
        {
            if(kategoris == null)
            {
                await SetKategoriDataSource();
                return;
            }

            var kategoriViewModels = await _kategoriService.GetAll();

            ViewBag.KategoriDataSource = kategoriViewModels.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nama,
                Selected = kategoris.FirstOrDefault(y => y == x.Id) == 0 ? false : true
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
                string fileName = string.Empty;

                if(dataInput.GambarFile != null)
                {
                    fileName = $"{Guid.NewGuid()}-{dataInput.GambarFile?.FileName}";
                    string filePathName = _iwebHost.WebRootPath + $"/images/{fileName}";

                    using (var StreamWriter = System.IO.File.Create(filePathName))
                    {
                        //await StreamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                        await StreamWriter.WriteAsync(dataInput.GambarFile.OpenReadStream().ToBytes());
                    }
                }

                var dataProduk = dataInput.ConvertToDbModel();
                dataProduk.Gambar = $"images/{fileName}";
                for (int i = 0; i < dataInput.KategoriId.Length; i++)
                {
                    dataProduk.KategoriProduks.Add(new Datas.Entities.KategoriProduk
                    {
                        IdKategori = dataInput.KategoriId[i],
                        IdProduk = dataProduk.Id,
                    });
                }


                await _produkService.Add(dataProduk);

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
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var produk = await _produkService.Get(id.Value);

            if (produk ==  null)
            {
                return NotFound();
            }
            var kategoriIds = await _produkKategoriService.GetKategoriIds(produk.Id);

            await SetKategoriDataSource(kategoriIds);



            var dataViewModel = new ProdukViewModel(produk);

            return View(dataViewModel);
        }

        // POST: Produks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProdukViewModel dataInput)
        {
            var dataProduk = dataInput.ConvertToDbModel();
            string fileName = string.Empty;

            if (dataInput.GambarFile != null)
            {
                fileName = $"{Guid.NewGuid()}-{dataInput.GambarFile?.FileName}";
                string filePathName = _iwebHost.WebRootPath + $"/images/{fileName}";

                using (var StreamWriter = System.IO.File.Create(filePathName))
                {
                    //await StreamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                    await StreamWriter.WriteAsync(dataInput.GambarFile.OpenReadStream().ToBytes());
                }
                dataProduk.Gambar = $"images/{fileName}";
            }
            else
            {
                dataProduk.Gambar = dataInput.Gambar;
            }

            for (int i = 0; i < dataInput.KategoriId.Length; i++)
            {
                dataProduk.KategoriProduks.Add(new Datas.Entities.KategoriProduk
                {
                    IdKategori = dataInput.KategoriId[i],
                    IdProduk = dataProduk.Id,
                });
            }

            await _produkService.Update(dataProduk);
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
            //var proudk = _context.Produks.Single(m => m.Id == id);
            //var a = _context.KategoriProduks.Where(m => m.IdProduk == id);
            //foreach (var i in a)
            //{
            //    _context.KategoriProduks.Remove(i);
            //}
            //_context.Remove(proudk);
            //_context.SaveChanges();
            await _produkService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
