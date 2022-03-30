namespace e_commerce.Interface;
using e_commerce.Datas.Entities;
using e_commerce.ViewModels;
    public interface IProdukService : ICrudService<Produk>
    {
    Task<Produk> Add(Produk obj, int idKategori);
    }
