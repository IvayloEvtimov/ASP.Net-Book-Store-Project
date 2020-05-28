using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
	public class Order
	{
		public int ID { get; set; }
		public long BookId { get; set; }
		public int CustomerId { get; set; }
		public int Amount { get; set; }
		public string Address { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime Date { get; set; }

		public Book Book { get; set; }
		public Customer Customer { get; set; }
	}
}
