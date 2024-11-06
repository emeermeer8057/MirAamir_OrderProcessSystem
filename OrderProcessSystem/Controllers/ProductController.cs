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
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		// GET: api/products
		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
		{
			Log.Information("Fetching all products.");
			var products = await _productService.GetAllProductsAsync();
			return Ok(products);
		}

		// GET: api/products/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			Log.Information($"Fetching product details for ID {id}.");
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
			{
				Log.Warning($"Product with ID {id} not found.");
				return NotFound($"Product with ID {id} not found.");
			}
			return Ok(product);
		}

		// POST api/products
		[HttpPost]
		public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDto)
		{
			if (productDto == null)
			{
				Log.Warning("Product data is null.");
				return BadRequest("Product data is required.");
			}

			Log.Information("Creating a new product with name: {ProductName}", productDto.Name);

			try
			{
				// Exclude ProductId from the input, ensure it's not explicitly set
				var product = new Product
				{
					Name = productDto.Name,
					Price = productDto.Price,
					
				};

				var createdProduct = await _productService.CreateProductAsync(product);

				Log.Information("Successfully created product with ID: {ProductId}", createdProduct.ProductId);

				// Return a DTO to the client with no ProductId included
				var result = new ProductDTO
				{
					Name = createdProduct.Name,
					Price = createdProduct.Price
				};

				return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, result);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error occurred while creating product.");
				return StatusCode(500, "An error occurred while processing your request.");
			}
		}
	}
}
