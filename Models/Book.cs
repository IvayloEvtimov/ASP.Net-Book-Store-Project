﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
	public class Book
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long ISBN { get; set; }
		public string Title { get; set; }

		[Display(Name ="Release Year")]
		public int ReleaseYear { get; set; }

		[ForeignKey("Genre")]
		public int GenreId { get; set; }
		public decimal Price { get; set; }
		public int Pages { get; set; }
		public string Info { get; set; }
		public string Cover{get;set;}

		public Genre Genre { get; set; }
		public Stockpile Stockpile { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<Cart> Carts { get; set; }
		public virtual ICollection<Written_By> BookAuthors { get; set;}
	}

}
