using e_commerce.Interface;
using e_commerce.Datas;
using e_commerce.Datas.Entities;
using e_commerce.ViewModels;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Services;

public class KategoriService : BaseDbService, IKategoriService
{
    public KategoriService(ecommerceContext dbContext) : base(dbContext)
    {

    }

    public async Task<Kategori> Add(Kategori obj)
    {
        if (await DbContext.Kategoris.AnyAsync(x => x.Id == obj.Id))
        {
            throw new InvalidOperationException($"Produk with ID {obj.Id} is already exist");
        }
        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();
        return obj;
    }

    public async Task<Kategori> Update(Kategori obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException("Kategori tidak boleh null");
        }

        var dataKategori = await DbContext.Kategoris.FirstOrDefaultAsync(x => x.Id == obj.Id);

        if (dataKategori == null)
        {
            throw new InvalidOperationException($"Kategori dengan ID {obj.Id} tidak ada di dalam database");
        }

        dataKategori.Id = obj.Id;
        dataKategori.Nama = obj.Nama;
        dataKategori.Deskripsi = obj.Deskripsi;
        dataKategori.Icon = obj.Icon;

        DbContext.Update(dataKategori);
        await DbContext.SaveChangesAsync();

        return dataKategori;
    }

    public async Task<bool> Delete(int id)
    {
        var dataKategori = await DbContext.Kategoris.FirstOrDefaultAsync(x => x.Id == id);

        if (dataKategori == null)
        {
            throw new InvalidOperationException($"Produk dengan ID {id} tidak ada");
        }

        // untuk delete dengan id=id
        DbContext.RemoveRange(DbContext.KategoriProduks.Where(x=>x.IdKategori == id));

        DbContext.Remove(dataKategori);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Kategori>> Get(int limit, int offset, string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
        {
            keyword = "";
        }

        return await DbContext.Kategoris.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<Kategori?> Get(int id)
    {
        var result = await DbContext.Kategoris.FirstOrDefaultAsync((x => x.Id == id));
        if (result == null)
        {
            throw new InvalidOperationException($"Produk dengan ID{id} tidak ada");
        };
        return result;
    }

    public Task<Kategori?> Get(Expression<Func<Kategori, bool>> func)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Kategori>> GetAll()
    {
        return await DbContext.Kategoris.ToListAsync();
    }
} 
