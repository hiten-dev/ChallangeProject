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
        ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService<CustomerDetail> customService, IFetchCustomer<Customer> fetchCustomer, 
            ILogger<CustomerController> logger)
        {
            _customService = customService;
            _fetchCustomer = fetchCustomer;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(CustomerDetail customer)
        {
            if (customer != null)
            {
                try
                {
                    _customService.Insert(customer);
                    return Ok();
                }
                catch(Exception e)
                {
                    _logger.LogError(e.Message, e);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unable to process the request");
                }

            }
            else
            {
                _logger.LogError("Unable to create Customer, invalid details provided.");
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid details");
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Fetching all records");
            return Ok(_fetchCustomer.GetAll());
        }
        [HttpGet]
        [Route("{guid:Guid}")]
        public IActionResult GetByID(Guid guid)
        {
            try
            {
                return Ok(_fetchCustomer.Get(guid));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to process the request");
            }
        }
        [HttpGet]
        [Route("{age:int}")]
        public IActionResult GetByAge(int age)
        {
            try
            {
                return Ok(_fetchCustomer.GetByAge(age));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to process the request");
            }
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
                    _logger.LogInformation("Successfully updated the customer details");
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message, e);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unable to process the request");
                }

            }
            else
            {
                _logger.LogError("Unable to update Customer details, invalid details provided.");
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid details");
            }
        }
    }
}
