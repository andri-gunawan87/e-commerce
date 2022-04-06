using e_commerce.Datas;
using e_commerce.Datas.Entities;
using e_commerce.Interface;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Services
{
    public class StatusService : BaseDbService, IStatusService
    {
        public StatusService(ecommerceContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<StatusOrder>> Get()
        {
            return await DbContext.StatusOrders.ToListAsync();
        }
    }
}
