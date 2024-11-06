using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderProcessSystem.Models
{
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public double TotalPrice { get; set; }
		public bool IsFulfilled { get; set; }

		// Foreign Key to Customer (Customer field is now optional)
		public int CustomerId { get; set; }

		// Navigation property for related Customer
		public Customer Customer { get; set; }

		public List<Product> Products { get; set; } = new List<Product>();

	}

	public class OrderRequest
	{
		public int CustomerId { get; set; }
		public List<int> ProductIds { get; set; }
	}
}
