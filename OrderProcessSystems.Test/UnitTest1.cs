using NUnit.Framework;
using Moq;
using OrderProcessSystem.Controllers;
using OrderProcessSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderProcessSystem.Models;

namespace OrderProcessTest
{
	[TestFixture]
	public class OrderControllerTests
	{
		private Mock<IOrderService> _mockOrderService;
		private OrderController _controller;

		[SetUp]
		public void SetUp()
		{
			_mockOrderService = new Mock<IOrderService>();
			_controller = new OrderController(_mockOrderService.Object);
		}

		[Test]
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

			Assert.IsInstanceOf<CreatedAtActionResult>(result);
			var createdAtActionResult = (CreatedAtActionResult)result;
			Assert.AreEqual(201, createdAtActionResult.StatusCode);
			Assert.AreEqual(order, createdAtActionResult.Value);
		}
	}
}
