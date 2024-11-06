using OrderProcessSystem.Models;

namespace OrderProcessSystem.Interfaces
{
	public interface IOrderService
	{
		Task<Order> CreateOrderAsync(int customerId, List<int> productIds);
		Task<Order> GetOrderByIdAsync(int orderId);
		Task<bool> FulfillOrderAsync(int orderId);
		Task<bool> IsLastOrderUnfulfilledAsync(int customerId);
	}

}
