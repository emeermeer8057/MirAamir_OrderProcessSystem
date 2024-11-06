using System.ComponentModel.DataAnnotations;

namespace OrderProcessSystem.Models
{
	public class Customer
	{
		public int CustomerId { get; set; }
		[Required]
		public string Name { get; set; }
		public string Email { get; set; }
 

	}
	public class CustomerDTO
	{
		public string Name { get; set; }
		public string Email { get; set; }
	}
}
