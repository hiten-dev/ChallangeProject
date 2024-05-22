using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace ChallengeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService<CustomerDetail> _customService;
        private readonly IFetchCustomer<Customer> _fetchCustomer;
        public CustomerController(ICustomerService<CustomerDetail> customService, IFetchCustomer<Customer> fetchCustomer)
        {
            _customService = customService;
            _fetchCustomer = fetchCustomer;
        }

        [HttpPost]
        public IActionResult Create(CustomerDetail customer)
        {
            if (customer != null)
            {
                try
                {
                    _customService.Insert(customer);
                    return StatusCode(StatusCodes.Status201Created);
                }
                catch(Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unable to process the request");
                }

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid details");
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_fetchCustomer.GetAll());
        }
        [HttpGet]
        [Route("{guid:Guid}")]
        public IActionResult GetByID(Guid guid)
        {
            return Ok(_fetchCustomer.Get(guid));
        }
        [HttpGet]
        [Route("{age:int}")]
        public IActionResult GetByAge(int age)
        {
            return Ok(_fetchCustomer.GetByAge(age));
        }
        [HttpPatch]
        [Route("{id:Guid}")]
        public IActionResult Update(Guid id, CustomerDetail customer)
        {
            if (customer != null)
            {
                try
                {
                    _customService.Update(id, customer);
                    return StatusCode(StatusCodes.Status200OK);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unable to process the request");
                }

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid details");
            }
        }
    }
}
