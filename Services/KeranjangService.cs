using e_commerce.Datas.Entities;
using e_commerce.Datas;
using e_commerce.ViewModels;
using e_commerce.Interface;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Services
{
    public class KeranjangService : BaseDbService, IkeranjangService
    {
        private readonly IProdukService _produkService;
        public KeranjangService(ecommerceContext dbContext, IProdukService produkService) : base(dbContext)
        {
            _produkService = produkService;
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<KeranjangViewModel> Add(KeranjangViewModel obj)
        {
            if (await DbContext.Keranjangs.AnyAsync(x => x.IdProduk == obj.IdProduk && x.IdCustomer == obj.IdCustomer))
            {
                return obj;
            }

            var produk = await _produkService.Get(obj.IdProduk);

            if (produk == null)
            {
                throw new InvalidOperationException("Data produk tidak ditemukan");
            }

            if (obj.JumlahBarang < 1)
            {
                obj.JumlahBarang = 1;
            }

            obj.SubTotal = produk.Harga * obj.JumlahBarang;
            await DbContext.Keranjangs.AddAsync(obj.ConvertToDbModel());
            await DbContext.SaveChangesAsync();

            return obj;
        }

        public async Task<bool> Delete(int id)
        {
            var keranjang = await DbContext.Keranjangs.FirstOrDefaultAsync(x => x.Id == id);

            if (keranjang == null)
            {
                throw new InvalidOperationException("cannot find cart item in database");
            }

            DbContext.Remove(keranjang);
            await DbContext.SaveChangesAsync();

            return true;
        }

        public Task<List<KeranjangViewModel>> Get(int limit, int offset, string keyword)
        {
            throw new NotImplementedException();
        }

        public async Task<KeranjangViewModel?> Get(int id)
        {
            var keranjang = await DbContext.Keranjangs.FirstOrDefaultAsync(x => x.Id == id);
            var dataViewModel = new KeranjangViewModel(keranjang);

            return dataViewModel;
        }

        public Task<KeranjangViewModel?> Get(Expression<Func<KeranjangViewModel, bool>> func)
        {
            throw new NotImplementedException();
        }

        public async Task<List<KeranjangViewModel>> GetAll()
        {
            var result = await DbContext.Keranjangs.ToListAsync();
            var dataKeranjang = new List<KeranjangViewModel>();
            foreach (Keranjang item in result)
            {
                dataKeranjang.Add(new KeranjangViewModel(item));
            }

            return dataKeranjang;
        }

        public async Task<KeranjangViewModel> Update(KeranjangViewModel obj)
        {
            var produk = await _produkService.Get(obj.IdProduk);
            var keranjang = await DbContext.Keranjangs.FirstOrDefaultAsync(x => x.Id == obj.Id);

            if (keranjang == null)
            {
                throw new InvalidOperationException("cannot find cart item in database");
            }

            //get data produk


            if (produk == null)
            {
                throw new InvalidOperationException("Produk tidak ditemukan");
            }

            if (obj.JumlahBarang < 1)
            {
                obj.JumlahBarang = 1;
            }

            //rumus subtotal = harga * jumlah produk
            keranjang.JumlahBarang = obj.JumlahBarang;
            keranjang.SubTotal = produk.Harga * keranjang.JumlahBarang;

            DbContext.Update(keranjang);
            await DbContext.SaveChangesAsync();
            var dataViewModel = new KeranjangViewModel(keranjang);

            return dataViewModel;
        }

        public async Task<List<KeranjangViewModel?>> GetKeranjang(int idCustomer)
        {
            if (idCustomer == null)
            {
                throw new ArgumentNullException("Produk cannot be null");
            }
            var result = await (from a in DbContext.Keranjangs
                                join b in DbContext.Produks on a.IdProduk equals b.Id
                                where a.IdCustomer == idCustomer
                                select new KeranjangViewModel
                                {
                                    Id = a.Id,
                                    IdCustomer = a.IdCustomer,
                                    IdProduk = a.IdProduk,
                                    Gambar = b.Gambar,
                                    JumlahBarang = a.JumlahBarang,
                                    SubTotal = a.SubTotal,
                                    NamaProduk = b.Nama
                                }).ToListAsync();
            return result;
        }
        public async Task Clear(int idCustomer)
        {
            DbContext.RemoveRange(DbContext.Keranjangs.Where(x => x.IdCustomer == idCustomer));
            await DbContext.SaveChangesAsync();
        }
    }
}
