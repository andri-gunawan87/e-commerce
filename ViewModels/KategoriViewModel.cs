using e_commerce.Datas.Entities;
namespace e_commerce.ViewModels
{
    public class KategoriViewModel
    {
        public int Id { get; set; }
        public string Nama { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;
        public string Icon { get; set; } = null!;

        public Kategori ConvertToDbModel()
        {
            return new Kategori
            {
                Id = this.Id,
                Nama = this.Nama,
                Deskripsi = this.Deskripsi,
                Icon = this.Icon,
            };
        }
    }
}
