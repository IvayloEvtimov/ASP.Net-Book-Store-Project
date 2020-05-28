namespace Project.Models
{
	public class Written_By
    {
        public long ISBN { get; set; }
        public int? AuthorID { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
