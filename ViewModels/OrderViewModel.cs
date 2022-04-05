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
        public decimal Total { get; set; }
        public string Status { get; set; }

        public List<OrderDetailViewModel> Details { get; set; }
    }
}
