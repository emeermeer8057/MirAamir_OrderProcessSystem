using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using OrderProcessSystem.Controllers;
using OrderProcessSystem.Models;
using OrderProcessSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderProcessSystem.Interfaces;

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
			// Mock the OrderService
			_mockOrderService = new Mock<IOrderService>();

			// Instantiate the controller with the mocked service
			_controller = new OrderController(_mockOrderService.Object);
		}

		[Test]
		public async Task TestCreateOrder_ReturnsCreatedAtActionResult()
		{
			// Arrange
			var request = new OrderRequest { CustomerId = 1, ProductIds = new List<int> { 1, 2 } };
			var mockOrder = new Order
			{
				OrderId = 1,
				TotalPrice = 100.00,
				CustomerId = 1,
				OrderDate = DateTime.UtcNow
			};

			_mockOrderService
				.Setup(service => service.CreateOrderAsync(It.IsAny<int>(), It.IsAny<List<int>>()))
				.ReturnsAsync(mockOrder);

			// Act
			var result = await _controller.CreateOrder(request);

			// Assert
			var createdResult = result as CreatedAtActionResult;
			Assert.NotNull(createdResult, "Expected CreatedAtActionResult but got a different result.");
			Assert.AreEqual(201, createdResult.StatusCode);
			Assert.AreEqual(mockOrder.OrderId, ((Order)createdResult.Value).OrderId);
		}

		[Test]
		public async Task TestCreateOrder_ReturnsBadRequest_WhenNoProducts()
		{
			// Arrange
			var request = new OrderRequest { CustomerId = 1, ProductIds = new List<int>() };

			// Act
			var result = await _controller.CreateOrder(request);

			// Assert
			Assert.IsInstanceOf<BadRequestObjectResult>(result);
		}

		[Test]
		public async Task TestCreateOrder_ThrowsException_ReturnsBadRequest()
		{
			// Arrange
			var request = new OrderRequest { CustomerId = 1, ProductIds = new List<int> { 1, 2 } };
			_mockOrderService
				.Setup(service => service.CreateOrderAsync(It.IsAny<int>(), It.IsAny<List<int>>()))
				.ThrowsAsync(new InvalidOperationException("Cannot create order"));

			// Act
			var result = await _controller.CreateOrder(request);

			// Assert
			Assert.is<BadRequestObjectResult>(result);
		}
	}
}
