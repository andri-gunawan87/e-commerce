using e_commerce.Datas.Entities;

namespace e_commerce.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            Details = new List<OrderDetailViewModel>();
        }

        

        public int Id { get; set; }
        public DateTime TanggalTransaksi { get; set; }
        public int JumlahBarang
        {
            get
            {
                return (Details == null || !Details.Any()) ? 0 : Details.Sum(x => x.JumlahBarang);
            }
        }
        public int IdAlamat { get; set; }
        public int IdCustomer { get; set; }
        public int IdStatus { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string? Catatan { get; set; }
        public string? NamaCustomer { get; set; }

        public List<OrderDetailViewModel> Details { get; set; }

        public OrderViewModel(Order item)
        {
            Id = item.Id;
            TanggalTransaksi = item.TanggalTransaksi;
            Total = item.JumlahBayar.Value;
            IdAlamat = item.IdAlamat;
            IdCustomer = item.IdCustomer;
            IdStatus = item.IdStatus;
            Catatan = item.Catatan;
        }
    }
}
