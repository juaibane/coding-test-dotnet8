using CustomerApi.Application.Dtos;
using CustomerApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        /// <summary>
        /// Adds a new list of customers.
        /// </summary>
        /// <remarks>
        /// Adds customers ensuring that there are no duplicate IDs
        /// and complies with business validation rules. Data is inserted sorted by last name and then by first name.
        /// </remarks>
        /// <param name="customers">The list of customers to add.</param>
        /// <returns>Result of the operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<CustomerDto> customers)
        {
            var result = await _customerService.AddCustomersAsync(customers);

            if (!result.Success)
                return StatusCode(result.StatusCode, new { Errors = result.Errors });

            return StatusCode(result.StatusCode);
        }

        /// <summary>
        /// Retrieves all stored customers.
        /// </summary>
        /// <remarks>
        /// Returns a list of customers with all fields.
        /// </remarks>
        /// <returns>A list of customers.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _customerService.GetAllAsync();
            if (!result.Success)
                return StatusCode(result.StatusCode, new { Errors = result.Errors });

            return StatusCode(result.StatusCode, result.Data);
        }

    }
}
