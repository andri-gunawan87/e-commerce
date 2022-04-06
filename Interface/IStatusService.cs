using e_commerce.Datas.Entities;

namespace e_commerce.Interface
{
    public interface IStatusService
    {
        Task<List<StatusOrder>> Get();
    }
}
