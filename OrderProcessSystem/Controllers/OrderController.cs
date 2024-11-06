using Microsoft.AspNetCore.Mvc;
using OrderProcessSystem.Interfaces;
using OrderProcessSystem.Models;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		// POST: api/orders
		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
		{
			if (request == null || request.ProductIds == null || request.ProductIds.Count == 0)
			{
				return BadRequest("Order data with valid product IDs is required.");
			}

			Log.Information($"Creating an order for customer ID {request.CustomerId}.");
			try
			{
				var order = await _orderService.CreateOrderAsync(request.CustomerId, request.ProductIds);
				return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
			}
			catch (InvalidOperationException ex)
			{
				Log.Error($"Order creation failed: {ex.Message}");
				return BadRequest(ex.Message);
			}
		}

		// GET: api/orders/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderById(int id)
		{
			Log.Information($"Fetching order details for ID {id}.");
			var order = await _orderService.GetOrderByIdAsync(id);
			if (order == null)
			{
				Log.Warning($"Order with ID {id} not found.");
				return NotFound($"Order with ID {id} not found.");
			}
			return Ok(order);
		}

		// PUT: api/orders/{id}/fulfill
		[HttpPut("{id}/fulfill")]
		public async Task<IActionResult> FulfillOrder(int id)
		{
			Log.Information($"Fulfilling order with ID {id}.");
			var result = await _orderService.FulfillOrderAsync(id);
			if (result)
			{
				return NoContent();
			}
			return NotFound($"Order with ID {id} not found.");
		}
	}
}
