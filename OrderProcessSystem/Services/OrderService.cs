using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderProcessSystem.Data;
using OrderProcessSystem.Interfaces;
using OrderProcessSystem.Models;
using Serilog;

namespace OrderProcessSystem.Services
{
	public class OrderService : IOrderService
	{
		private readonly OrderContext _context;

		public OrderService(OrderContext context)
		{
			_context = context;
		}

		public async Task<Order> CreateOrderAsync(int customerId, List<int> productIds)
		{
			Log.Information($"Creating order for customer {customerId}.");

			// Check if the customer exists
			var customer = await _context.Customers.FindAsync(customerId);
			if (customer == null)
			{
				throw new InvalidOperationException($"Customer with ID {customerId} does not exist.");
			}

			// Check if the last order for this customer is unfulfilled
			if (await IsLastOrderUnfulfilledAsync(customerId))
			{
				throw new InvalidOperationException("Cannot create order. Last order is unfulfilled.");
			}

			// Retrieve the products based on the productIds
			var products = await _context.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync();

			// Ensure that at least one product exists
			if (products.Count == 0)
			{
				throw new InvalidOperationException("No valid products found for the given product IDs.");
			}

			// Create the order
			var order = new Order
			{
				CustomerId = customerId,
				OrderDate = DateTime.UtcNow,
				Products = products
			};

			// Calculate total price of the order
			order.TotalPrice = (double)order.Products.Sum(p => p.Price);  

			// Add the order to the context
			_context.Orders.Add(order);

			// Save changes to the database
			await _context.SaveChangesAsync();

			return order;
		}

		public async Task<Order> GetOrderByIdAsync(int orderId)
		{
			Log.Information($"Retrieving order {orderId}");
			return await _context.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.OrderId == orderId);
		}

		public async Task<bool> FulfillOrderAsync(int orderId)
		{
			Log.Information($"Fulfilling order {orderId}");
			var order = await _context.Orders.FindAsync(orderId);
			if (order == null) return false;
			order.IsFulfilled = true;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> IsLastOrderUnfulfilledAsync(int customerId)
		{
			var lastOrder = await _context.Orders
				.Where(o => o.CustomerId == customerId)
				.OrderByDescending(o => o.OrderDate)
				.FirstOrDefaultAsync();

			return lastOrder != null && !lastOrder.IsFulfilled;
		}
	}
}
