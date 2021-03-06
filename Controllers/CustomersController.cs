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
    public class CustomersController : BaseController
    {
        private readonly ecommerceContext _context;

        public CustomersController(ecommerceContext context)
        {
            _context = context;
        }

        // GET: Customers
        [Authorize(Roles = AppConstant.ADMIN)]
        public async Task<IActionResult> Index()
        {
            var ecommerceContext = _context.Customers.Include(c => c.IdAlamatNavigation);
            return View(await ecommerceContext.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.IdAlamatNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["IdAlamat"] = new SelectList(_context.Alamats.Where(x => x.IdUser == id), "Id", "Detail", customer.IdAlamat);

            return View(customer);
            //return View("~/Views/Home/Profile.cshtml");
        }

        // GET: Customers/Create
        [Authorize(Roles = AppConstant.ADMIN)]
        public IActionResult Create()
        {
            ViewData["IdAlamat"] = new SelectList(_context.Alamats, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nama,NoHp,Username,Password,ProfilPicture,Email,IsAdmin,IdAlamat")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlamat"] = new SelectList(_context.Alamats, "Id", "Id", customer.IdAlamat);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["IdAlamat"] = new SelectList(_context.Alamats, "Id", "Id", customer.IdAlamat);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,NoHp,ProfilPicture,IdAlamat")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["IdAlamat"] = new SelectList(_context.Alamats, "Id", "Id", customer.IdAlamat);
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = AppConstant.ADMIN)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.IdAlamatNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [Authorize(Roles = AppConstant.ADMIN)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAlamat(int id, int idAlamat)
        {
            if (id == 0 || idAlamat == 0)
            {
                return NotFound();
            }
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                customer.IdAlamat = idAlamat;
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return NotFound();
            }

            return NotFound();
        }
    }
}
