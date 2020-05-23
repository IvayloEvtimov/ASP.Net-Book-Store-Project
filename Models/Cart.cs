using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
	public class Cart
	{
		public int Customer_ID { get; set; }
		public Customer Customer { get; set; }

		[Display(Name="Product")]
		public long ISBN { get; set; }
		public Book Book { get; set; }

		public int Volume { get; set; }


	}
}