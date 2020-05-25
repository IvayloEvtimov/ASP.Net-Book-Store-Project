
using System.Collections.Generic;

namespace Project.Models
{
	public class BookAuthorIndexData
	{
		public IEnumerable<Book> Books { get; set; }
		public IEnumerable<Author> Authors { get; set; }
		public IEnumerable<Written_By> BookAuthors { get; set; }
 	}
}