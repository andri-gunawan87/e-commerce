using e_commerce.Datas.Entities;
namespace e_commerce.ViewModels
{
    public class KategoriViewModel
    {
        public KategoriViewModel()
        {
            Nama = string.Empty;
            Deskripsi = string.Empty;
        }
        public KategoriViewModel(Kategori item)
        {
            Id = item.Id;
            Nama = item.Nama;
            Deskripsi = item.Deskripsi;
            Icon = item.Icon;
        }
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
