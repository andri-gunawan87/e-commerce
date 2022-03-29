using e_commerce.Datas;

namespace e_commerce.Services
{
    public class BaseDbService
    {
        protected readonly ecommerceContext DbContext;
        public BaseDbService(ecommerceContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
