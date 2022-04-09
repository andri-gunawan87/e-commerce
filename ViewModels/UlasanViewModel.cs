using e_commerce.Datas.Entities;

namespace e_commerce.ViewModels
{
    public class UlasanViewModel
    {
        public UlasanViewModel()
        {

        }

        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdCustomer { get; set; }
        public string Komentar { get; set; } = null!;
        public string? Gambar { get; set; } = null!;
        public int Rating { get; set; }
        public IFormFile FileUlasan { get; set; }
        public Ulasan ConvertToDbModel()
        {
            return new Ulasan
            {
                Id = this.Id,
                IdOrder = this.IdOrder,
                IdCustomer = this.IdCustomer,
                Komentar = this.Komentar,
                Gambar = this.Gambar,
                Rating = this.Rating,
            };
        }
    }
}
