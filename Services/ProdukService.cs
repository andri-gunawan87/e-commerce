using e_commerce.Interface;
using e_commerce.Datas;
using e_commerce.Datas.Entities;
using e_commerce.ViewModels;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Services;
public class ProdukService : BaseDbService, IProdukService
{
    public ProdukService(ecommerceContext dbContext) : base(dbContext)
    {

    }

    public async Task<Produk> Add(Produk obj, int idKategori)
    {

        if (await DbContext.Produks.AnyAsync(x => x.Id == obj.Id))
        {
            throw new InvalidOperationException($"Produk with ID {obj.Id} is already exist");
        }
        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();
        DbContext.KategoriProduks.Add(new KategoriProduk
        {
            IdKategori = idKategori,
            Id = obj.Id,
        });
        return obj;
    }
    public async Task<Produk> Add(Produk obj)
    {
        if (await DbContext.Produks.AnyAsync(x => x.Id == obj.Id))
        {
            throw new InvalidOperationException($"Produk with ID {obj.Id} is already exist");
        }

        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<Produk> Update(Produk obj)
    {
        if(obj == null)
        {
            throw new ArgumentNullException("Produk cannot be null");
        }

        var dataProduk = await DbContext.Produks.FirstOrDefaultAsync(x => x.Id == obj.Id);

        if (dataProduk == null)
        {
            throw new InvalidOperationException($"Produk with ID{obj.Id} doesnt exist in database");
        }
        dataProduk.Id = obj.Id;
        dataProduk.Nama = obj.Nama;
        dataProduk.Deskripsi = obj.Deskripsi;
        dataProduk.Harga = obj.Harga;
        dataProduk.Stock = obj.Stock;
        dataProduk.Gambar = obj.Gambar;

        DbContext.Update(dataProduk);
        await DbContext.SaveChangesAsync();

        return dataProduk;
    }

    public async Task<bool> Delete(int id)
    {
        //var proudk = _context.Produks.Single(m => m.Id == id);
        //var a = _context.KategoriProduks.Where(m => m.IdProduk == id);
        //foreach (var i in a)
        //{
        //    _context.KategoriProduks.Remove(i);
        //}
        //_context.Remove(proudk);
        //_context.SaveChanges();

        var dataproduk = await DbContext.Produks.FirstOrDefaultAsync(x => x.Id == id);
        var dataKategoriProduk = await DbContext.KategoriProduks.Where(s => s.IdProduk == id).ToListAsync();
        if (dataproduk == null)
        {
            throw new InvalidOperationException($"Produk dengan ID {id} tidak ada");
        }

        foreach (var item in dataKategoriProduk)
        {
            DbContext.Remove(item);
        }
        DbContext.Remove(dataproduk);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Produk>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword))
        {
            keyword = "";
        }

        return await DbContext.Produks.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<Produk?> Get(int id)
    {
        var result = await DbContext.Produks.FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
        {
            throw new InvalidOperationException($"Produk dengan ID{id} tidak ada");
        };
        return result;
    }

    public Task<Produk?> Get(Expression<Func<Produk, bool>> func)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Produk>> GetAll()
    {
        return await DbContext.Produks.ToListAsync();
    }
}


