using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
	public class Book
	{
		public long ISBN { get; set; }
		public string Title { get; set; }
		public int ReleaseYear { get; set; }
		public int GenreId { get; set; }
		public decimal Price { get; set; }
		public int Pages { get; set; }
		public string Info { get; set; }

		public Genre Genre { get; set; }
		public Stockpile Stockpile { get; set; }

		public ICollection<Order> Orders { get; set; }
	}

}
