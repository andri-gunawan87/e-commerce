using e_commerce.Interface;
using e_commerce.Datas;
using e_commerce.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Services;
public class ProdukKategoriService : BaseDbService, IProdukKategoriService
{
    public ProdukKategoriService(ecommerceContext dbContext) : base(dbContext)
    {
    }

    public async Task<int[]> GetKategoriIds(int produkId)
    {
        var result = await DbContext.KategoriProduks
            .Where(x => x.IdProduk == produkId)
            .Select(x => x.IdKategori)
            .Distinct().ToArrayAsync();
        return result;
    }
}