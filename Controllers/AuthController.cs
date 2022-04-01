using Microsoft.AspNetCore.Mvc;
using e_commerce.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using e_commerce.Interface;
using e_commerce.Datas.Entities;

namespace e_commerce.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            var dbResult = await _authService.GetAll();


            //for (int i = 0; i < dbResult.Count; i++)
            //{
            //    viewModels.Add(new KategoriViewModel
            //    {
            //        Id = dbResult[i].Id,
            //        Nama = dbResult[i].Nama,
            //        Deskripsi = dbResult[i].Deskripsi,
            //        Icon = dbResult[i].Icon,
            //    });
            //}
            return View(dbResult);
        }

        public IActionResult Login()
        {
            return View(new AccountLoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel dataInput)
        {
            var result = await _authService.Login(dataInput.Username, dataInput.Password);
            string roleUser = string.Empty;
            if (result.IsAdmin)
            {
                roleUser = AppConstant.ADMIN;
            }
            else
            {
                roleUser = AppConstant.USER;
            }

            if (result == null)
            {
                return View(new AccountLoginViewModel());
            }

            try
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.Email ?? result.Nama),
                    new Claim("FullName", result.Nama),
                    new Claim(ClaimTypes.Role, roleUser)
                };

                var claimsIdentity = new ClaimsIdentity(claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToActionPermanent("Index", "Home");
            }
            catch (System.Exception)
            {
                return View(dataInput);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }


        // Fungsi Register Akun
        public IActionResult Register()
        {

            return View(new AccountRegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel dataLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(dataLogin);
            }
            try
            {
                await _authService.Add(dataLogin);

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

            return View(dataLogin);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var datainput = await _authService.Get(id);
            
            if (datainput == null)
            {
                return NotFound();
            }

            return View(datainput);
        }

        // POST: Kategoris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AccountRegisterViewModel dataInput)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dataInput);
            }
            try
            {
                var dataKategori = await _authService.Update(dataInput);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View(dataInput);
        }
        // Fungsi Delete
        public async Task<IActionResult> Delete(int id)
        {
            var dataUser = await _authService.Get(id);
            return View(dataUser);
        }

        // POST: Kategoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
