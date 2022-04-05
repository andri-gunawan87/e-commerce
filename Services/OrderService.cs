using e_commerce.Datas.Entities;
using e_commerce.Datas;
using e_commerce.Interface;

namespace e_commerce.Services
{
    public class OrderService : BaseDbService, IOrderService
    {
        public OrderService(ecommerceContext dbContext): base(dbContext)
        {

        }
        public async Task<Order> CheckOut(Order newOrder)
        {
            await DbContext.AddAsync(newOrder);
            await DbContext.SaveChangesAsync();
            return newOrder;
        }
    }
}
