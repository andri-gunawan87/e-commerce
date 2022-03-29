namespace e_commerce.Interface;
using e_commerce.Datas.Entities;
    public interface IProdukService : ICrudService<Produk>
    {
    Task<Produk> Add(Produk obj, int idKategori);
    }
