using OrderProcessSystem.Models;

namespace OrderProcessSystem.Interfaces
{
	public interface ICustomerService
	{
		Task<IEnumerable<Customer>> GetAllCustomersAsync();
		Task<Customer> GetCustomerByIdAsync(int customerId);
		Task<Customer> CreateCustomerAsync(Customer customer);
	}
}
