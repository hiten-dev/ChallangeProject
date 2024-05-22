
namespace Service.Interface
{
    public interface ICustomerService<T> where T : class
    {
        Guid Insert(T entity);
        void Update(Guid id, T entity);
    }
}
