using ChallengeApplication.Controllers;
using ChallengeApplication.Data;
using Domain.Models;
using Repository;
using Service;
using Service.Interface;

namespace ChallengeApplicationTest
{
    public class CustomerControllerTest
    {
        CustomerController _controller;
        ICustomerService<Customer> _service;
        IRepository<Customer> _repository;

        public CustomerControllerTest()
        {
            //DataContext dataContext = new DataContext();
            //_repository = new Repository<Customer>(dataContext);
            //_service = new CustomerService();
            //_controller = new CustomerController();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}