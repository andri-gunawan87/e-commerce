using e_commerce.Datas.Entities;
namespace e_commerce.ViewModels
{
    public class KeranjangViewModel
    {
        public KeranjangViewModel()
        {
            Gambar = string.Empty;
            NamaProduk = string.Empty;
        }

        public int Id { get; set; }
        public int IdProduk { get; set; }
        public int IdCustomer { get; set; }
        public int JumlahBarang { get; set; }
        public decimal SubTotal { get; set; }
        public string? Gambar { get; set; }
        public string? NamaProduk { get; set; }

        public Keranjang ConvertToDbModel()
        {
            return new Keranjang
            {
                Id = this.Id,
                IdProduk = this.IdProduk,
                IdCustomer = this.IdCustomer,
                JumlahBarang = this.JumlahBarang,
                SubTotal = this.SubTotal
            };
        }
        public KeranjangViewModel(Keranjang item)
        {
            Id = item.Id;
            IdProduk = item.IdProduk;
            IdCustomer = item.IdCustomer;
            JumlahBarang = item.JumlahBarang;
            SubTotal = item.SubTotal;
        }

    }
}
