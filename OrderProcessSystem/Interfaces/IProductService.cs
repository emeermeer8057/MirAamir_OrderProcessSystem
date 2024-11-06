using OrderProcessSystem.Models;

namespace OrderProcessSystem.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetAllProductsAsync();
		Task<Product> GetProductByIdAsync(int productId);
		Task<Product> CreateProductAsync(Product product);
	}
}
