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
using Microsoft.AspNetCore.Authorization;

namespace e_commerce.Controllers
{
    public class KeranjangsController : BaseController
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
            if (User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
                var customer = _context.Customers.FirstOrDefault(x => x.Id == userId);
                ViewData["IdAlamat"] = new SelectList(_context.Alamats.Where(x => x.IdUser == userId), "Id", "Detail", customer.IdAlamat);
                var dbResult = await _keranjangService.GetKeranjang(GetId());
                return View(dbResult);
            }
            


            return View();
            //return View(await _context.Keranjangs.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int JumlahBarang, int IdProduk)
        {
            if (IdProduk == 0)
            {
                return BadRequest();
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Name).ToString(); ;
            var userData = _context.Customers.FirstOrDefault(x => x.Email == userEmail);
            int userId = userData.Id;

            await _keranjangService.Add(new KeranjangViewModel
            {
                IdProduk = IdProduk,
                JumlahBarang = JumlahBarang,
                IdCustomer = GetId()
            });

            return RedirectToAction(nameof(Index));
        }

        // GET: Keranjangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keranjangViewModel = await _keranjangService.Get(id.Value);
            
            return View(keranjangViewModel);
        }

        // POST: Keranjangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KeranjangViewModel keranjangViewModel)
        {
            if (id != keranjangViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dataInput = await _keranjangService.Update(keranjangViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeranjangViewModelExists(keranjangViewModel.Id))
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
            return View(keranjangViewModel);
        } 
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJumlah(int IdProduk, int JumlahBarang)
        {
            var dataExist = await _context.Keranjangs.FirstOrDefaultAsync(x => x.Id == IdProduk);
            var dataProduk = await _context.Produks.FirstOrDefaultAsync(x => x.Id == dataExist.IdProduk);
            dataExist.JumlahBarang = JumlahBarang;
            dataExist.SubTotal = JumlahBarang * dataProduk.Harga;
            await _context.SaveChangesAsync();


            return Redirect(nameof(Index));
        }

        // GET: Keranjangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var dataInput = _context.Keranjangs.FirstOrDefault(x => x.Id == id);
            var dataViewModel = new KeranjangViewModel(dataInput);

            return View(dataViewModel);
        }

        // POST: Keranjangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataViewModel = await _keranjangService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool KeranjangViewModelExists(int id)
        {
            return _context.Keranjangs.Any(e => e.Id == id);
        }

        public int GetId()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name).ToString(); ;
            var userData = _context.Customers.FirstOrDefault(x => x.Email == userEmail);
            int userId = userData.Id;

            return userId;
        }
    }
}
