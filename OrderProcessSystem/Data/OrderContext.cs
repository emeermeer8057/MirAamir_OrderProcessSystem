using Microsoft.EntityFrameworkCore;
using OrderProcessSystem.Models;

namespace OrderProcessSystem.Data
{
	public class OrderContext : DbContext
	{
		public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure many-to-many relationship between Order and Product
			modelBuilder.Entity<Order>()
				.HasMany(o => o.Products);

		}
	}
}
