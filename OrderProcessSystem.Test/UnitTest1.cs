 
using Moq;
using OrderProcessSystem.Controllers;
using OrderProcessSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderProcessSystem.Models;

namespace OrderProcessSystem.Test
{
	[TestFixture] // Marks the class as a test fixture for NUnit
	public class OrderControllerTests
	{
		private Mock<IOrderService> _mockOrderService;
		private OrderController _controller;

		[SetUp] // Called before each test
		public void SetUp()
		{
			_mockOrderService = new Mock<IOrderService>();
			_controller = new OrderController(_mockOrderService.Object);
		}

		[Test] // Marks this method as a test
		public async Task CreateOrder_ReturnsCreatedAtActionResult_WhenSuccessful()
		{
			var orderRequest = new OrderRequest
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1, 2 }
			};

			var order = new Order { OrderId = 123, TotalPrice = 200 };
			_mockOrderService.Setup(s => s.CreateOrderAsync(orderRequest.CustomerId, orderRequest.ProductIds)).ReturnsAsync(order);

			var result = await _controller.CreateOrder(orderRequest);

			Assert.That(result, Is.Not.Null);

			var createdAtActionResult = result as CreatedAtActionResult;

			Assert.That(createdAtActionResult, Is.Not.Null);
	 
		}
	}
}
