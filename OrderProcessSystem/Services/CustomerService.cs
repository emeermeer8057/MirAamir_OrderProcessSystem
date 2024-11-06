using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;  
using OrderProcessSystem.Data;
using OrderProcessSystem.Interfaces;
using OrderProcessSystem.Models;
using Serilog;

namespace OrderProcessSystem.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly OrderContext _context;

		public CustomerService(OrderContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
		{
			Log.Information("Retrieving all customers.");
			return await _context.Customers.ToListAsync();
		}

		public async Task<Customer> CreateCustomerAsync(Customer customer)
		{
			// Ensure the Customer object is valid
			if (customer == null)
			{
				throw new ArgumentNullException(nameof(customer), "Customer data cannot be null.");
			}

			// Don't set CustomerId, let the database auto-generate it
			_context.Customers.Add(customer);

			// Save the new customer to the database
			await _context.SaveChangesAsync();

			// Return the created customer object with the automatically generated CustomerId
			return customer;
		}

		public async Task<Customer> GetCustomerByIdAsync(int customerId)
		{
			return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
		}

	}
}
