using Domain.Models;
using Domain.ViewModels;
using Repository;
using RestSharp;
using Service.Interface;

namespace Service
{
    public class CustomerService : ICustomerService<CustomerDetail>
    {
        private IRepository<Customer> _repository;
        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        Guid ICustomerService<CustomerDetail>.Insert(CustomerDetail entity)
        {
            try
            {
                Customer customer = new Customer()
                {
                    CustomerId = new Guid(),
                    Name = entity.Name,
                    DateOfBirth = entity.DateOfBirth,
                    ImagePath = string.Empty
                };
                _repository.Insert(customer);
                _repository.SaveChanges();
                string imgPath = GetImage(customer.CustomerId, entity.Name).Result;
                customer.ImagePath = imgPath;
                _repository.Update(customer);
                _repository.SaveChanges();
                return customer.CustomerId;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static async Task<string> GetImage(Guid guid, string Name)
        {
            try
            {
                var url = string.Format("https://ui-avatars.com/api/?name={0}&format=svg", Name);
                var client = new RestClient(url);
                var request = new RestRequest();
                RestResponse response = await client.ExecuteAsync(request);
                var imageName = string.Format(@"Images\{0}.svg", guid.ToString().Replace('-', '_'));
                var path = Path.Combine(Directory.GetCurrentDirectory(), imageName);
                    if (response.RawBytes != null)
                    {
                        File.WriteAllBytes(path, response.RawBytes);
                        return imageName;
                    }
                    else
                    {
                        return string.Empty;
                    }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        void ICustomerService<CustomerDetail>.Update(Guid id, CustomerDetail entity)
        {
            try
            {
                var existingdetail = _repository.Get(id);
                Customer customer = new Customer()
                {
                    CustomerId = id,
                    Name = string.IsNullOrEmpty(entity.Name) ? existingdetail.Name : entity.Name,
                    DateOfBirth = entity.DateOfBirth == DateOnly.MinValue ? existingdetail.DateOfBirth : entity.DateOfBirth,
                    ImagePath = existingdetail.ImagePath
                };
                _repository.Update(customer);
                _repository.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
