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
using Microsoft.AspNetCore.Authorization;

namespace e_commerce.Controllers
{
    public class OrderController : BaseController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IkeranjangService _keranjangService;
        private readonly IOrderService _orderService;
        private readonly ecommerceContext _context;
        private readonly IStatusService _statusService;
        private readonly IWebHostEnvironment _iwebHost;

        public OrderController(ILogger<OrderController> logger,
            IkeranjangService keranjangService,
            IOrderService orderService,
            ecommerceContext ecommerceContext,
            IStatusService statusService,
            IWebHostEnvironment iwebHost)
        {
            _logger = logger;
            _keranjangService = keranjangService;
            _orderService = orderService;
            _context = ecommerceContext;
            _statusService = statusService;
            _iwebHost = iwebHost;

        }

        public async Task<IActionResult> Index(int? page, int? pageCount)
        {

            if (User.IsInRole(AppConstant.ADMIN))
            {
                var tupplePagination = Common.ToLimitOffset(page, pageCount);
                var result = await _orderService.GetFilteredAdmin(tupplePagination.Item1, tupplePagination.Item2);
                await SetStatusListAsSelectListItem();
                ViewBag.FilterDate = null;
                return View(result);
            }
            else
            {
                var result = await _orderService.GetAllCustomer(GetId());
                return View(result);
            }


            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromQuery] int? page, [FromQuery] int? pageCount, int? status, DateTime? date)
        {
            var tuplePagination = Common.ToLimitOffset(page, pageCount);
            var result = await _orderService.GetFilteredAdmin(tuplePagination.Item1, tuplePagination.Item2, status, date);
            await SetStatusListAsSelectListItem(status);
            if (date != null)
            {
                ViewBag.FilterDate = date.Value.ToString("MM/dd/yyyy");
            }
            return View(result);
        }

        private async Task SetStatusListAsSelectListItem(int? status = null)
        {
            var statusList = await _statusService.Get();

            if (statusList == null || !statusList.Any())
            {
                ViewBag.StatusList = new List<SelectListItem>();
            }
            else
            {
                ViewBag.StatusList = statusList.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nama,
                    Selected = status != null && status.Value == x.Id
                }).ToList();
            }
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
            //var dataDetailOrder = await (from a in _context.Orders
            //                             join b in _context.StatusOrders on a.IdStatus equals b.Id
            //                             join c in _context.Alamats on a.IdAlamat equals c.Id
            //                             select new OrderViewModel
            //                             {
            //                                 Id = a.Id,
            //                                 Status = b.Nama,
            //                                 TanggalTransaksi = a.TanggalTransaksi,
            //                                 Total = a.JumlahBayar.Value,
            //                                 Details = (from d in _context.DetailOrders
            //                                            join e in _context.Produks
            //            on d.IdProduk equals e.Id
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
            //ViewBag.DataOrder = _context.Orders.FirstOrDefault(x => x.Id == id);
            //var result = await _context.DetailOrders.Where(x => x.IdOrder == id).ToListAsync();
            var result = await _orderService.GetDetailOrder(id);
            return View(result);
        }

        public async Task<IActionResult> PembayaranTransaksi(int id)
        {
            //var dataDetailOrder = await (from a in _context.Orders
            //                             join b in _context.StatusOrders on a.IdStatus equals b.Id
            //                             join c in _context.Alamats on a.IdAlamat equals c.Id
            //                             select new OrderViewModel
            //                             {
            //                                 Id = a.Id,
            //                                 Status = b.Nama,
            //                                 TanggalTransaksi = a.TanggalTransaksi,
            //                                 Total = a.JumlahBayar.Value,
            //                                 Details = (from d in _context.DetailOrders
            //                                            join e in _context.Produks
            //            on d.IdProduk equals e.Id
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
            ViewBag.DataOrder = _context.Orders.FirstOrDefault(x => x.Id == id);
            var result = await _context.DetailOrders.Where(x => x.IdOrder == id).ToListAsync();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Pembayaran(PembayaranViewModel dataInput)
        {
            if (!ModelState.IsValid)
            {
                return View(dataInput);
            }
            try
            {
                string fileName = string.Empty;

                if (dataInput.FileBukti != null)
                {
                    fileName = $"{Guid.NewGuid()}-{dataInput.FileBukti?.FileName}";
                    string filePathName = _iwebHost.WebRootPath + $"/BuktiTransfer/{fileName}";

                    using (var StreamWriter = System.IO.File.Create(filePathName))
                    {
                        await StreamWriter.WriteAsync(dataInput.FileBukti.OpenReadStream().ToBytes());
                    }
                }

                var dataBayar = dataInput.ConvertToDbModel();
                dataBayar.BuktiPembayaran = $"BuktiTransfer/{fileName}";
                dataBayar.Status = "Menunggu Konfirmasi";
                dataBayar.Tanggal = DateTime.Now;

                await _orderService.Dibayar(dataBayar);

                // update data order
                var dataOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == dataInput.IdOrder);
                dataOrder.IdStatus = 2;
                _context.Update(dataOrder);
                await _context.SaveChangesAsync();

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

        public async Task<IActionResult> DetailPembayaran(int id)
        {
            var dataPembayaran = await _orderService.GetDetailPembayaran(id);
            return View(dataPembayaran);

        }
        [Authorize(Roles = AppConstant.ADMIN)]
        [HttpPost]
        public async Task<IActionResult> KonfirmasiPembayaran(int id)
        {
            ViewBag.dataOrder = await _orderService.KonfirmasiOrder(id);
            return RedirectToAction("Index", "Order");
        }

        [Authorize(Roles = AppConstant.ADMIN)]
        public async Task<IActionResult> CreatePengiriman(int id)
        {
            var result = await _context.DetailOrders.Where(x => x.IdOrder == id).ToListAsync();
            ViewBag.dataOrder = await _orderService.GetOrder(id);
            return View(result);
        }

        [Authorize(Roles = AppConstant.ADMIN)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pengiriman(PengirimanViewModel dataInput)
        {
            if(!ModelState.IsValid)
            {
                throw new Exception();
            }

            var dataPengiriman = dataInput.ConvertToDbModel();
           
            await _orderService.Dikirim(dataPengiriman);

            return Redirect(nameof(Index));
        }

        public async Task<IActionResult> DataPengiriman(int id)
        {
            ViewBag.DataPengiriman = _context.Pengirimen.FirstOrDefault(x => x.IdOrder == id);
            var result = await _context.DetailOrders.Where(x => x.IdOrder == id).ToListAsync();
            return View(result);
        } 
        
        [HttpPost]
        public async Task<IActionResult> KonfirmasiDiterima(int idOrder)
        {
            await _orderService.Diterima(idOrder);
            return Redirect(nameof(Index));
        }

        public async Task<IActionResult> BeriUlasan(int id)
        {
            ViewBag.DataOrder = _context.Orders.FirstOrDefault(x => x.Id == id);
            var result = await _context.DetailOrders.Where(x => x.IdOrder == id).ToListAsync();
            return View(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> SimpanUlasan(UlasanViewModel dataUlasan)
        {
            if (!ModelState.IsValid)
            {
                return View(dataUlasan);
            }
            try
            {
                string fileName = string.Empty;

                if (dataUlasan.FileUlasan != null)
                {
                    fileName = $"{Guid.NewGuid()}-{dataUlasan.FileUlasan?.FileName}";
                    string filePathName = _iwebHost.WebRootPath + $"/ReviewImage/{fileName}";

                    using (var StreamWriter = System.IO.File.Create(filePathName))
                    {
                        //await StreamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                        await StreamWriter.WriteAsync(dataUlasan.FileUlasan.OpenReadStream().ToBytes());
                    }
                }

                var dataBayar = dataUlasan.ConvertToDbModel();
                dataBayar.Gambar = $"ReviewImage/{fileName}";
               
                await _orderService.Diulas(dataBayar);

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

            return View(dataUlasan);
        }

        public int GetId()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
            return userId;
        }
    }
}
