using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using OrderProcessSystem.Controllers;
using OrderProcessSystem.Interfaces;
using OrderProcessSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace OrderProcessSystem.Tests
{
	public class OrderControllerTests
	{
		private readonly Mock<IOrderService> _mockOrderService;
		private readonly OrderController _controller;

		public OrderControllerTests()
		{
			_mockOrderService = new Mock<IOrderService>();
			_controller = new OrderController(_mockOrderService.Object);
		}

		[Fact] // Marks this as an xUnit test method
		public async Task CreateOrder_ReturnsCreatedAtActionResult_WhenSuccessful()
		{
			// Arrange
			var orderRequest = new OrderRequest
			{
				CustomerId = 1,
				ProductIds = new List<int> { 1, 2 }
			};

			var order = new Order { OrderId = 123, TotalPrice = 200 };
			_mockOrderService.Setup(s => s.CreateOrderAsync(orderRequest.CustomerId, orderRequest.ProductIds)).ReturnsAsync(order);

			// Act
			var result = await _controller.CreateOrder(orderRequest);

			// Assert
			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
			Assert.Equal(201, createdAtActionResult.StatusCode);
			Assert.Equal(order, createdAtActionResult.Value);
		}

		[Fact] // Another test case
		public async Task CreateOrder_ReturnsBadRequest_WhenRequestIsInvalid()
		{
			// Arrange
			var orderRequest = new OrderRequest
			{
				CustomerId = 1,
				ProductIds = null
			};

			// Act
			var result = await _controller.CreateOrder(orderRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("Order data with valid product IDs is required.", badRequestResult.Value);
		}

		[Fact]
		public async Task GetOrderById_ReturnsNotFound_WhenOrderDoesNotExist()
		{
			// Arrange
			var orderId = 999; // Non-existent order ID
			_mockOrderService.Setup(s => s.GetOrderByIdAsync(orderId)).ReturnsAsync((Order)null);

			// Act
			var result = await _controller.GetOrderById(orderId);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			Assert.Equal($"Order with ID {orderId} not found.", notFoundResult.Value);
		}

		[Fact]
		public async Task FulfillOrder_ReturnsNoContent_WhenSuccessful()
		{
			// Arrange
			var orderId = 123;
			_mockOrderService.Setup(s => s.FulfillOrderAsync(orderId)).ReturnsAsync(true);

			// Act
			var result = await _controller.FulfillOrder(orderId);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task FulfillOrder_ReturnsNotFound_WhenOrderDoesNotExist()
		{
			// Arrange
			var orderId = 999; // Non-existent order ID
			_mockOrderService.Setup(s => s.FulfillOrderAsync(orderId)).ReturnsAsync(false);

			// Act
			var result = await _controller.FulfillOrder(orderId);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			Assert.Equal($"Order with ID {orderId} not found.", notFoundResult.Value);
		}
	}
}
