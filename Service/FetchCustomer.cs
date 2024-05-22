using Domain.Models;
using Domain.ViewModels;
using Repository;
using Service.Interface;

namespace Service
{
    public class FetchCustomer : IFetchCustomer<Customer>
    {
        private IRepository<Customer> _repository;
        public FetchCustomer(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public Customer Get(Guid Id) => _repository.Get(Id);

        public IEnumerable<Customer> GetAll() => _repository.GetAll();

        public IEnumerable<Customer> GetByAge(int age)
        {
            try
            {
                var mindate = DateOnly.FromDateTime(DateTime.Now.AddYears(-(age+1)));
                var maxdate = DateOnly.FromDateTime(DateTime.Now.AddYears(-(age)));
                return _repository.GetAll().Where(x => x.DateOfBirth <= maxdate && x.DateOfBirth >= mindate);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
