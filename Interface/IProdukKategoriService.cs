namespace e_commerce.Interface;
using e_commerce.Datas.Entities;
public interface IProdukKategoriService
{
    Task<int[]> GetKategoriIds(int produkId);
    Task Remove(int produkId, int idKategori);
}