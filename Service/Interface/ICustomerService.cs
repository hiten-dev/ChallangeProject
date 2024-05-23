
namespace Service.Interface
{
    public interface ICustomerService<T> where T : class
    {
        Task<Guid> Insert(T entity);
        Task Update(Guid id, T entity);
    }
}
