
namespace Service.Interface
{
    public interface IFetchCustomer<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(Guid Id);
        public Task<IEnumerable<T>> GetByAge(int age);
    }
}
