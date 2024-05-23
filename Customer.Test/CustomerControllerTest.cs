using ChallengeApplication.Controllers;
using Domain.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;
using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Test
{
    public class CustomerControllerTest
    {
        private readonly Mock<ICustomerService<CustomerDetail>> _mockCustomerService;
        private readonly Mock<IFetchCustomer<Domain.Models.Customer>> _mockFetchCustomer;
        private readonly Mock<ILogger<CustomerController>> _mockLogger;
        private readonly CustomerController _controller;
        public CustomerControllerTest()
        {
            _mockCustomerService = new Mock<ICustomerService<CustomerDetail>>();
            _mockFetchCustomer = new Mock<IFetchCustomer<Domain.Models.Customer>>();
            _mockLogger = new Mock<ILogger<CustomerController>>();

            _controller = new CustomerController((ICustomerService<CustomerDetail>)_mockCustomerService,
                (IFetchCustomer<Domain.Models.Customer>)_mockFetchCustomer, (ILogger<CustomerController>)_mockLogger);
        }

        [Fact]
        public async Task Get_AllOkResult()
        {

            _mockFetchCustomer.Setup(service => service.GetAll()).Returns(new List<Domain.Models.Customer>());
            var result = _controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task Get_ByAgeOkResult()
        {

            _mockFetchCustomer.Setup(service => service.GetByAge(30)).Returns(new List<Domain.Models.Customer>());
            var result = _controller.GetByAge(30);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}