using e_commerce.Datas.Entities;
using e_commerce.Datas;
using e_commerce.Interface;
using e_commerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Services
{
    public class OrderService : BaseDbService, IOrderService
    {
        public OrderService(ecommerceContext dbContext) : base(dbContext)
        {

        }
        public async Task<Order> CheckOut(Order newOrder)
        {
            await DbContext.AddAsync(newOrder);
            await DbContext.SaveChangesAsync();
            return newOrder;
        }

        public async Task<Pembayaran> Dibayar(Pembayaran dataInput)
        {
            await DbContext.AddAsync(dataInput);
            await DbContext.SaveChangesAsync();
            return dataInput;
        }

        public async Task<PembayaranViewModel> GetDetailPembayaran(int idOrder)
        {
            var dataPembayaran = await DbContext.Pembayarans.FirstOrDefaultAsync(x => x.IdOrder == idOrder);
            var dataViewModel = new PembayaranViewModel(dataPembayaran);
            return dataViewModel;
        }

        public async Task<List<OrderDetailViewModel>> GetDetailOrder(int idOrder)
        {
            var result = await (from a in DbContext.Orders
                                join b in DbContext.DetailOrders on a.Id equals b.IdOrder
                                join c in DbContext.Produks on b.IdProduk equals c.Id
                                where a.Id == idOrder
                                select new OrderDetailViewModel
                                {
                                    Id = b.Id,
                                    Produk = c.Nama,
                                    Harga = c.Harga,
                                    JumlahBarang = b.JumlahBarang,
                                    SubTotal = b.SubTotal,
                                    Gambar = c.Gambar
                                   
                                }).ToListAsync();

            return result;
        }


        public async Task<List<OrderViewModel>> GetAllCustomer(int idCustomer)
        {
            var result = await (from a in DbContext.Orders
                                join b in DbContext.StatusOrders on a.IdStatus equals b.Id
                                join alamat in DbContext.Alamats on a.IdAlamat equals alamat.Id
                                where a.IdCustomer == idCustomer
                                select new OrderViewModel
                                {
                                    Id = a.Id,
                                    IdStatus = a.IdStatus,
                                    Status = b.Nama,
                                    TanggalTransaksi = a.TanggalTransaksi,
                                    Total = a.JumlahBayar.Value,
                                    Details = (from c in DbContext.DetailOrders
                                               join d in DbContext.Produks on c.IdProduk equals d.Id
                                               where c.IdOrder == a.Id
                                               select new OrderDetailViewModel
                                               {
                                                   Id = c.Id,
                                                   Produk = d.Nama,
                                                   Harga = c.Harga,
                                                   JumlahBarang = c.JumlahBarang,
                                                   SubTotal = c.SubTotal
                                               }).ToList()
                                }).ToListAsync();

            return result;
        }

        public async Task<List<OrderViewModel>> GetFilteredAdmin(int limit, int offset, int? status = null, DateTime? date = null)
        {
            var selectCondition = (from a in DbContext.Orders
                                   join b in DbContext.StatusOrders on a.IdStatus equals b.Id
                                   join alamat in DbContext.Alamats on a.IdAlamat equals alamat.Id
                                   join c in DbContext.Customers on a.IdCustomer equals c.Id
                                   select new OrderViewModel
                                   {
                                       Id = a.Id,
                                       Status = b.Nama,
                                       TanggalTransaksi = a.TanggalTransaksi,
                                       NamaCustomer = c.Nama,
                                       Total = a.JumlahBayar.Value,
                                       IdStatus = a.IdStatus,
                                       Catatan = a.Catatan,
                                   }).AsQueryable();

            if (status != null)
            {
                selectCondition = selectCondition.Where(x => x.IdStatus == status.Value);
            }

            if (date != null)
            {
                selectCondition = selectCondition.Where(x => x.TanggalTransaksi.Date == date.Value.Date);
            }

            return await selectCondition
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
        }

        public async Task<OrderViewModel> GetOrder(int idOrder)
        {
            var dataOrder = await DbContext.Orders.FirstOrDefaultAsync(x => x.Id == idOrder);
            var dataViewModel = new OrderViewModel(dataOrder);
            return dataViewModel;
        }

        public async Task<OrderViewModel> KonfirmasiOrder(int idOrder)
        {
            var dataOrder = await DbContext.Orders.FirstOrDefaultAsync(x => x.Id == idOrder);
            dataOrder.IdStatus = 3;
            DbContext.Update(dataOrder);
            DbContext.SaveChanges();
            var dataViewModel = new OrderViewModel(dataOrder);
            return dataViewModel;
        }

        public async Task<Pengiriman> Dikirim(Pengiriman dataInput)
        {
            await DbContext.AddAsync(dataInput);
            await DbContext.SaveChangesAsync();

            var dataOrder = await DbContext.Orders.FirstOrDefaultAsync(x => x.Id == dataInput.IdOrder);
            dataOrder.IdStatus = 4;
            DbContext.Update(dataOrder);
            DbContext.SaveChanges();

            return dataInput;
        }

        public async Task Diterima(int idOrder)
        {
            var dataOrder = await DbContext.Orders.FirstOrDefaultAsync(x => x.Id == idOrder);
            dataOrder.IdStatus = 5;
            DbContext.Update(dataOrder);
            DbContext.SaveChanges();
        }

        public async Task<Ulasan> Diulas(Ulasan dataInput)
        {
            await DbContext.AddAsync(dataInput);
            await DbContext.SaveChangesAsync();

            var dataOrder = await DbContext.Orders.FirstOrDefaultAsync(x => x.Id == dataInput.IdOrder);
            dataOrder.IdStatus = 6;
            DbContext.Update(dataOrder);
            DbContext.SaveChanges();

            return dataInput;
        }
    }
}
