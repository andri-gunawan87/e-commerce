using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using e_commerce.Datas.Entities;
using e_commerce.ViewModels;
using e_commerce.Interface;
using System.Security.Claims;

namespace e_commerce.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly e_commerce.Datas.ecommerceContext _dbContext;
        private readonly IProdukService _produkService;

        public HomeController(ILogger<HomeController> logger,
            e_commerce.Datas.ecommerceContext dbContext,
            IProdukService produkService
            )
        {
            _dbContext = dbContext;
            _logger = logger;
            _produkService = produkService;
        }

        // Halaman Index
        public async Task<IActionResult> Index(int? page, int? pageCount)
        {
            var viewModels = new List<ProdukViewModel>();

            int limit = 4;
            if (pageCount != null)
            {
                limit = pageCount.Value;
            }

            int offset = 0;
            if (page == null)
            {
                offset = 0;
            }
            else
            {
                offset = (page.Value - 1) * limit;
            }
            var result = await _produkService.Get(limit, offset, String.Empty);

            if (result == null || !result.Any())
            {
                return RedirectToAction(nameof(Index), new
                {
                    page = page > 1 ? page - 1 : 1,
                    pageCount = pageCount
                });
            }

            foreach (var x in result)
            {
                viewModels.Add(new ProdukViewModel
                {
                    Id = x.Id,
                    Nama = x.Nama,
                    Deskripsi = x.Deskripsi,
                    Harga = x.Harga,
                    Stock = x.Stock,
                    Gambar = x.Gambar,
                });
                
                //return View(await _dbContext.Produks.ToListAsync());
            }

            ViewBag.HalamanSekarang = page ?? 1;
            return View(viewModels);
        }

            // Fungsi Register Akun
            public IActionResult Register()
            {

                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Register(Customer dataLogin)
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Add(dataLogin);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Users");
                }

                return RedirectToAction("Index");
            }

            // Fungsi Add Product
            public IActionResult AddProduct()
            {

                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddProduct(Produk dataProduct)
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Add(dataProduct);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(dataProduct);
            }

            // Fungsi Detail Product
            public async Task<IActionResult> DetailProduct(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var dataProuct = await _dbContext.Produks
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (dataProuct == null)
                {
                    return NotFound();
                }

                return View(dataProuct);
            }

            // Fungsi Edit Product
            // GET: ecommerce/Edit/5
            public async Task<IActionResult> EditProduct(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var dataProduct = await _dbContext.Produks.FindAsync(id);
                if (dataProduct == null)
                {
                    return NotFound();
                }
                return View(dataProduct);
            }

            // POST: ecommerce/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditProduct(int id, [Bind("Id,Nama,Deskripsi,Harga,Stock,Gambar")] Produk dataProduct)
            {
                if (id != dataProduct.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _dbContext.Update(dataProduct);
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!(_dbContext.Produks.Any(e => e.Id == id)))
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
                return View(dataProduct);
            }

            // Fungsi Delete
            // Halaman Konfirmasi Delete
            public async Task<IActionResult> DeleteProduct(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var dataProduct = await _dbContext.Produks
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (dataProduct == null)
                {
                    return NotFound();
                }

                return View(dataProduct);
            }

            // POST: ecommerce/Delete/5
            [HttpPost, ActionName("DeleteProduct")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteProductConfirmed(int id)
            {

                var dataProduct = await _dbContext.Produks.FindAsync(id);
                _dbContext.Produks.Remove(dataProduct);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }



        }
    }