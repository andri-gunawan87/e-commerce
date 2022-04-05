using Microsoft.AspNetCore.Mvc;
using e_commerce.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using e_commerce.Helpers;
using e_commerce.Datas.Entities;
using e_commerce.Datas;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using e_commerce.ViewModels;

namespace e_commerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IkeranjangService _keranjangService;
        private readonly IOrderService _orderService;
        private readonly ecommerceContext _context;

        public OrderController(ILogger<OrderController> logger, 
            IkeranjangService keranjangService, 
            IOrderService orderService,
            ecommerceContext ecommerceContext)
        {
            _logger = logger;
            _keranjangService = keranjangService;
            _orderService = orderService;
            _context = ecommerceContext;

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (HttpContext.User == null || HttpContext.User.Identity == null)
            {
                ViewBag.IsLogged = false;
            }
            else
            {
                ViewBag.IsLogged = HttpContext.User.Identity.IsAuthenticated;
            }

            base.OnActionExecuted(context);
        }


        public async Task<IActionResult> Index()
        {

            if (User.IsInRole(AppConstant.ADMIN))
            {
                var result = await _context.Orders.ToListAsync();
                return View(result);
            }
            else
            {
                var result = await _context.Orders.Where(x => x.IdCustomer == GetId()).ToListAsync();
                return View(result);
            }


            return NotFound();
        }
        
        public async Task<IActionResult> Create()
        {
            var userId = GetId();
            var customer = _context.Customers.FirstOrDefault(x => x.Id == userId);
            ViewData["IdAlamat"] = new SelectList(_context.Alamats.Where(x => x.IdUser == userId), "Id", "Detail", customer.IdAlamat);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel dataCheckOut)
        {
            int idCustomer = GetId();
            var dataUser = _context.Customers.FirstOrDefault(x => x.Id == idCustomer);
            var result = await _keranjangService.GetKeranjang(idCustomer);

            if (result == null)
            {
                return BadRequest();
            }

            var newOrder = new Order();

            newOrder.IdCustomer = idCustomer;
            newOrder.JumlahBayar = result.Sum(x => x.SubTotal);
            newOrder.Catatan = string.Empty;
            newOrder.IdStatus = 1;
            newOrder.IdAlamat = dataCheckOut.IdAlamat;
            newOrder.TanggalTransaksi = DateTime.Now;
            newOrder.DetailOrder = new List<DetailOrder>();

            foreach (var item in result)
            {
                newOrder.DetailOrder.Add(new DetailOrder
                {
                    IdOrder = newOrder.Id,                    
                    JumlahBarang = item.JumlahBarang,
                    SubTotal = item.SubTotal,
                    Harga = (item.SubTotal / item.JumlahBarang),
                    IdProduk = item.IdProduk
                });
            }

            await _orderService.CheckOut(newOrder);

            await _keranjangService.Clear(idCustomer);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DetailOrder(int id)
        {
            //var dataDetailOrder = await (from a in _context.Orders join b in _context.StatusOrders on a.IdStatus equals b.Id
            //                             join c in _context.Alamats on a.IdAlamat equals c.Id select new OrderViewModel
            //                             {
            //                                 Id = a.Id,
            //                                 Status = b.Nama,
            //                                 TanggalTransaksi = a.TanggalTransaksi,
            //                                 Total = a.JumlahBayar.Value,
            //                                 Details = (from d in _context.DetailOrders join e in _context.Produks
            //                                            on d.IdProduk equals e.Id
            //                                            where d.IdOrder == a.Id
            //                                            select new OrderDetailViewModel
            //                                            {
            //                                                Id = d.Id,
            //                                                Produk = e.Nama,
            //                                                Harga = d.Harga,
            //                                                JumlahBarang = d.JumlahBarang,
            //                                                SubTotal = d.SubTotal
            //                                            }).ToList()
            //                             }).ToListAsync();
            //return View(dataDetailOrder);
            var result = await _context.DetailOrders.Where(x => x.IdOrder == id).ToListAsync();
            return View(result);
        }

        public int GetId()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
            return userId;
        }
    }
}
