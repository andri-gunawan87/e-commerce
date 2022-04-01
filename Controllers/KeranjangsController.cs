#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce.Datas;
using e_commerce.ViewModels;
using e_commerce.Interface;
using e_commerce.Datas.Entities;
using System.Security.Claims;
using e_commerce.Helpers;

namespace e_commerce.Controllers
{
    public class KeranjangsController : Controller
    {
        private readonly ecommerceContext _context;
        private readonly IkeranjangService _keranjangService;

        public KeranjangsController(ecommerceContext context, IkeranjangService ikeranjangService)
        {
            _context = context;
            _keranjangService = ikeranjangService;
        }

        // GET: Keranjangs
        public async Task<IActionResult> Index()
        {
            var dbResult = await _keranjangService.GetAll();

            //var viewModels = new List<Keranjang>();

            //foreach (KeranjangViewModel item in dbResult)
            //{
            //    viewModels.Add(new KeranjangViewModel(item));
            //}

            return View(dbResult);
            //return View(await _context.Keranjangs.ToListAsync());
        }

        //// GET: Keranjangs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var keranjangViewModel = await _context.KeranjangViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (keranjangViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(keranjangViewModel);
        //}

        // GET: Keranjangs/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Keranjangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            //var data = new KeranjangViewModel();
            //data.JumlahBarang = 1;
            //data.IdProduk = id;
            //data.IdCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();

            await _keranjangService.Add(new KeranjangViewModel
            {
                IdProduk = id,
                JumlahBarang = 1,
                IdCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt()
            });

            return RedirectToAction(nameof(Index));
        }

        //// GET: Keranjangs/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var keranjangViewModel = await _context.KeranjangViewModel.FindAsync(id);
        //    if (keranjangViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(keranjangViewModel);
        //}

        //// POST: Keranjangs/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,IdProduk,IdCustomer,JumlahBarang,SubTotal,Gambar,NamaProduk")] KeranjangViewModel keranjangViewModel)
        //{
        //    if (id != keranjangViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(keranjangViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!KeranjangViewModelExists(keranjangViewModel.Id))
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
        //    return View(keranjangViewModel);
        //}

        //// GET: Keranjangs/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var keranjangViewModel = await _context.KeranjangViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (keranjangViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(keranjangViewModel);
        //}

        //// POST: Keranjangs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var keranjangViewModel = await _context.KeranjangViewModel.FindAsync(id);
        //    _context.KeranjangViewModel.Remove(keranjangViewModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool KeranjangViewModelExists(int id)
        //{
        //    return _context.KeranjangViewModel.Any(e => e.Id == id);
        //}
    }
}
