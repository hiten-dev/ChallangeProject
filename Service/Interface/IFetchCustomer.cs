
namespace Service.Interface
{
    public interface IFetchCustomer<T> where T : class
    {
        public IEnumerable<T> GetAll(); 
        public T Get(Guid Id);
        public IEnumerable<T> GetByAge(int age);
    }
}
