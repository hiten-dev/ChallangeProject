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

        public Task<Customer> Get(Guid Id) => _repository.Get(Id);

        public Task<IEnumerable<Customer>> GetAll() => _repository.GetAll();

        public async Task<IEnumerable<Customer>> GetByAge(int age)
        {
            try
            {
                var mindate = DateOnly.FromDateTime(DateTime.Now.AddYears(-(age + 1)));
                var maxdate = DateOnly.FromDateTime(DateTime.Now.AddYears(-(age)));
                return (await _repository.GetAll()).Where(x => x.DateOfBirth <= maxdate && x.DateOfBirth >= mindate);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
