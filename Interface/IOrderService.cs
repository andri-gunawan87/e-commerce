using e_commerce.Datas.Entities;
using e_commerce.ViewModels;

namespace e_commerce.Interface
{
    public interface IOrderService
    {
        Task<Order> CheckOut(Order newOrder);
    }
}
