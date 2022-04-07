using e_commerce.Datas.Entities;

namespace e_commerce.ViewModels
{
    public class PengirimanViewModel
    {
        public PengirimanViewModel()
        {

        }
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public string Kurir { get; set; } = null!;
        public decimal Ongkir { get; set; }
        public int IdAlamat { get; set; }
        public int? Status { get; set; }
        public string Keterangan { get; set; } = null!;
        public string NoResi { get; set; } = null!;

        public Pengiriman ConvertToDbModel()
        {
            return new Pengiriman
            {
                Id = this.Id,
                IdOrder = this.IdOrder,
                Kurir = this.Kurir,
                Ongkir = this.Ongkir,
                IdAlamat = this.IdAlamat,
                Status = this.Status,
                Keterangan = this.Keterangan,
                NoResi = this.NoResi,
            };
        }
    }
}
