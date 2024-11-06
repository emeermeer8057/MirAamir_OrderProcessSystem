using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderProcessSystem.Data;
using OrderProcessSystem.Interfaces;
using OrderProcessSystem.Models;
using Serilog;
using Serilog.Core;

namespace OrderProcessSystem.Services
{
	public class ProductService : IProductService
	{
		private readonly OrderContext _context;

		public ProductService(OrderContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Product>> GetAllProductsAsync()
		{
			Log.Information("Retrieving all products.");
			return await _context.Products.ToListAsync();
		}

		public async Task<Product> CreateProductAsync(Product product)
		{
			if (product == null)
			{
				Log.Warning("Received null product for creation.");
				throw new ArgumentNullException(nameof(product), "Product data cannot be null.");
			}

			try
			{
				// Do not include ProductId, as it's an auto-generated identity column
				_context.Products.Add(product);
				await _context.SaveChangesAsync();

				Log.Information("Successfully created product with ID: {ProductId}", product.ProductId);
				return product;
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error occurred while creating product.");
				throw;
			}
		}

		public async Task<Product> GetProductByIdAsync(int productId)
		{
			try
			{
				var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

				if (product == null)
				{
					Log.Warning("Product not found with ID: {ProductId}", productId);
					return null;
				}

				Log.Information("Successfully retrieved product with ID: {ProductId}", productId);
				return product;
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error occurred while retrieving product with ID: {ProductId}", productId);
				throw;
			}
		}
	}
}
