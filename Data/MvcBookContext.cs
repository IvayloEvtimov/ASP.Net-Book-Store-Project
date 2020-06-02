using Microsoft.EntityFrameworkCore;
using Project.Models;


namespace Project.Data
{
	public class MvcBookContext : DbContext
	{
		public MvcBookContext(DbContextOptions<MvcBookContext> options):base(options)
		{

		}

		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Stockpile> Stockpiles { get; set; }
		public DbSet<Written_By> BookAuthors { get; set; }
		public DbSet<Cart> Carts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Author>().ToTable("Author");
			modelBuilder.Entity<Book>().ToTable("Book");
			modelBuilder.Entity<Customer>().ToTable("Customer");
			modelBuilder.Entity<Genre>().ToTable("Genre");
			modelBuilder.Entity<Order>().ToTable("Orders");
			modelBuilder.Entity<Stockpile>().ToTable("Stockpile");
			modelBuilder.Entity<Written_By>().ToTable("Written_By");
			modelBuilder.Entity<Cart>().ToTable("Cart");

			modelBuilder.Entity<Book>().Property(model => model.Price).HasColumnType("float");
			modelBuilder.Entity<Book>().HasOne(model => model.Genre).WithMany(model => model.Books).IsRequired();

			modelBuilder.Entity<Written_By>().HasKey(model => new { model.ISBN, model.AuthorID });
			modelBuilder.Entity<Written_By>().HasOne(model => model.Book).WithMany(model => model.BookAuthors).HasForeignKey(model => model.ISBN);
			modelBuilder.Entity<Written_By>().HasOne(model => model.Author).WithMany(model => model.Written_Books).HasForeignKey(model => model.AuthorID);

			modelBuilder.Entity<Cart>().HasKey(model => new { model.Customer_ID, model.ISBN });
			modelBuilder.Entity<Cart>().HasOne(model => model.Customer).WithMany(model => model.Carts).HasForeignKey(model => model.Customer_ID);
			modelBuilder.Entity<Cart>().HasOne(model => model.Book).WithMany(model => model.Carts).HasForeignKey(model => model.ISBN);

			modelBuilder.Entity<Order>().HasOne(model => model.Book).WithMany(model => model.Orders).HasForeignKey(model => model.BookId);
			modelBuilder.Entity<Order>().HasOne(model => model.Customer).WithMany(model => model.Orders).HasForeignKey(model => model.CustomerId);	

		}
	}
}