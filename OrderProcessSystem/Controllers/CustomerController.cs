using Microsoft.AspNetCore.Mvc;
using Serilog; 
using OrderProcessSystem.Interfaces;
using OrderProcessSystem.Models;

namespace OrderProcessSystem.Controller
{
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpPost]
		[Route("api/customers")]
		public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO customerDto)
		{

			// Create the customer entity from the DTO
			var customer = new Customer
			{
				Name = customerDto.Name,
				Email = customerDto.Email
			};
			// Check if the customer data is valid (not null)
			if (customer == null)
			{
				return BadRequest("Customer data is required.");
			}

			// Log the creation action
			Log.Information("Creating a new customer with the name: {CustomerName}", customer.Name);

			// Call the service to create a new customer
			var createdCustomer = await _customerService.CreateCustomerAsync(customer);

			// Return a response with the created customer details
			return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.CustomerId }, createdCustomer);
		}

		// Get customer by ID endpoint (to be used in CreatedAtAction)
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCustomerById(int id)
		{
			var customer = await _customerService.GetCustomerByIdAsync(id);
			if (customer == null)
			{
				return NotFound();
			}
			return Ok(customer);
		}
	}
}
