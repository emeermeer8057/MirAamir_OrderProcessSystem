using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderProcessSystem.Models
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	 
	}
	public class ProductDTO
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public decimal Price { get; set; } 
	}
}
