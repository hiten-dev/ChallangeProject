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
        async Task<Guid> ICustomerService<CustomerDetail>.Insert(CustomerDetail entity)
        {
            try
            {
                // I have used custom mapping but we can also use DTO here
                Customer customer = new Customer()
                {
                    CustomerId = new Guid(),
                    Name = entity.Name,
                    DateOfBirth = entity.DateOfBirth,
                    ImagePath = string.Empty
                };
                await _repository.Insert(customer);
                await _repository.SaveChanges();

                //Getting svg image after customer created successfully
                string imgPath = GetImage(customer.CustomerId, entity.Name).Result;
                customer.ImagePath = imgPath;
                await _repository.Update(customer);
                await _repository.SaveChanges();
                return customer.CustomerId;
            }
            catch (Exception)
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

        async Task ICustomerService<CustomerDetail>.Update(Guid id, CustomerDetail entity)
        {
            try
            {
                // I have used custom mapping but we can also use DTO here
                var existingdetail = await _repository.Get(id);
                existingdetail.Name = string.IsNullOrEmpty(entity.Name) ? existingdetail.Name : entity.Name;
                existingdetail.DateOfBirth = entity.DateOfBirth == DateOnly.MinValue ? existingdetail.DateOfBirth : entity.DateOfBirth;
                await _repository.Update(existingdetail);
                await _repository.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
