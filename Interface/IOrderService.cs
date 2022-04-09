using e_commerce.Datas.Entities;
using e_commerce.ViewModels;

namespace e_commerce.Interface
{
    public interface IOrderService
    {
        Task<Order> CheckOut(Order newOrder);
        Task<Pembayaran> Dibayar(Pembayaran dataInput);
        Task<Pengiriman> Dikirim(Pengiriman dataInput);
        Task Diterima(int idOrder);
        Task<Ulasan> Diulas(Ulasan dataInput);
        Task<PembayaranViewModel> GetDetailPembayaran(int idOrder);
        Task<OrderViewModel> GetOrder(int idOrder);
        Task<OrderViewModel> KonfirmasiOrder(int idOrder);
        Task<List<OrderViewModel>> GetAllCustomer(int idCustomer);
        Task<List<OrderViewModel>> GetFilteredAdmin(int limit, int offset, int? status = null, DateTime? date = null);
        Task<List<OrderDetailViewModel>> GetDetailOrder(int idOrder);
    }
}
