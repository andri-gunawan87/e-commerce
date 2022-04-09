#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce.Datas;
using e_commerce.Datas.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using e_commerce.Helpers;
using e_commerce.Interface;
using e_commerce.ViewModels;

namespace e_commerce.Controllers
{
    public class AlamatsController : BaseController
    {
        private readonly ecommerceContext _context;
        private readonly IAlamatService _alamatService;

        public AlamatsController(ecommerceContext context, IAlamatService alamatService)
        {
            _context = context;
            _alamatService = alamatService;
        }

        // GET: Alamats
        [Authorize(Roles = AppConstant.ADMIN)]
        public async Task<IActionResult> Index()
        {
            var result = await _alamatService.GetAll();
            return View(result);
        }

        public async Task<IActionResult> AlamatUser()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
            var result = await _alamatService.GetUserAlamat(userId);
            var data = _context.Alamats.Where(x => x.IdUser == userId).Include(c => c.IdUser).ToList();

            return View(data);
        }

        // GET: Alamats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alamat = await _alamatService.Get(id.Value);
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
        public async Task<IActionResult> Create(AlamatViewModel dataInput)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
                dataInput.IdUser = userId;

                var result = await _alamatService.Add(dataInput);
                return RedirectToAction(nameof(Index));
            }
            return View(dataInput);
        }

        // GET: Alamats/Edit/5
        [Authorize(Roles = AppConstant.ADMIN)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alamat = await _alamatService.Get(id.Value);
            if (alamat == null)
            {
                return NotFound();
            }
            return View(alamat);
        }

        // POST: Alamats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = AppConstant.ADMIN)]
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
                    var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
                    alamat.IdUser = userId;
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

            var alamat = await _alamatService.Get(id.Value);
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
            await _alamatService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AlamatExists(int id)
        {
            return _context.Alamats.Any(e => e.Id == id);
        }
    }
}
