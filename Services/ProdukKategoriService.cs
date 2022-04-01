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

    public async Task Remove(int produkId, int idKategori)
    {
        var item = await DbContext.KategoriProduks.FirstOrDefaultAsync(x => x.IdProduk == produkId && x.IdKategori == idKategori);

        if ( item == null)
        {
            return;
        }
        DbContext.KategoriProduks.Remove(item);
        await DbContext.SaveChangesAsync();
    }
}