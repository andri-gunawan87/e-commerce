using e_commerce.Datas.Entities;
using e_commerce.Datas;
using e_commerce.ViewModels;
using e_commerce.Interface;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Services
{
    public class KeranjangService : BaseDbService, IkeranjangService
    {
        private readonly IProdukService _produkService;
        public KeranjangService(ecommerceContext dbContext, IProdukService produkService) : base(dbContext)
        {
            _produkService = produkService;
        }

        public async Task<KeranjangViewModel> Add(KeranjangViewModel obj)
        {
            if (await DbContext.Keranjangs.AnyAsync(x => x.IdProduk == obj.IdProduk && x.IdCustomer == obj.IdCustomer))
            {
                return obj;
            }

            var produk = await _produkService.Get(obj.Id);

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
            throw new NotImplementedException();
        }

        public Task<List<KeranjangViewModel>> Get(int limit, int offset, string keyword)
        {
            throw new NotImplementedException();
        }

        public Task<KeranjangViewModel?> Get(int id)
        {
            throw new NotImplementedException();
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
                dataKeranjang.Add( new KeranjangViewModel(item));
            }

            return dataKeranjang;
        }

        public Task<KeranjangViewModel> Update(KeranjangViewModel obj)
        {
            throw new NotImplementedException();
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
    }
}
