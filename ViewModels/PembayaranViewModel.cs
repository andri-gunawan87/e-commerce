using e_commerce.Datas.Entities;

namespace e_commerce.ViewModels
{
    public class PembayaranViewModel
    {
        public PembayaranViewModel()
        {

        }

        public int Id { get; set; }
        public string MetodePembayaran { get; set; } = null!;
        public decimal TotalBayar { get; set; }
        public int IdOrder { get; set; }
        public int IdCustomer { get; set; }
        public DateTime Tanggal { get; set; }
        public string Tujuan { get; set; } = null!;
        public int? Pajak { get; set; }
        public string? Status { get; set; }
        public IFormFile FileBukti { get; set; }
        public string? BuktiPembayaran { get; set; }

        public Pembayaran ConvertToDbModel()
        {
            return new Pembayaran
            {
                Id = this.Id,
                MetodePembayaran = this.MetodePembayaran,
                TotalBayar = this.TotalBayar,
                IdOrder = this.IdOrder,
                IdCustomer = this.IdCustomer,
                Tanggal = this.Tanggal,
                Tujuan = this.Tujuan,
                Pajak = this.Pajak,
                Status = this.Status,
                BuktiPembayaran = this.BuktiPembayaran,
            };
        }

        public PembayaranViewModel(Pembayaran item)
        {
            Id = item.Id;
            MetodePembayaran = item.MetodePembayaran;
            TotalBayar = item.TotalBayar;
            IdOrder = item.IdOrder;
            IdCustomer = item.IdCustomer;
            Tanggal = item.Tanggal;
            Tujuan = item.Tujuan;
            Pajak = item.Pajak;
            Status = item.Status;
            BuktiPembayaran = item.BuktiPembayaran;
        }
    }
}
