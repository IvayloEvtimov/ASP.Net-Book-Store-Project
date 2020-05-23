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

            modelBuilder.Entity<Book>().Property(b => b.Price).HasColumnType("float");

            modelBuilder.Entity<Written_By>().HasKey(c => new { c.ISBN, c.AuthorID });
			modelBuilder.Entity<Written_By>().HasOne(c => c.Book).WithMany(c => c.BookAuthors).HasForeignKey(bc => bc.ISBN);
			modelBuilder.Entity<Written_By>().HasOne(c => c.Author).WithMany(c => c.Written_Books).HasForeignKey(bc => bc.AuthorID);

			modelBuilder.Entity<Cart>().HasKey(c => new { c.Customer_ID, c.ISBN });
			modelBuilder.Entity<Cart>().HasOne(c => c.Customer).WithMany(c => c.Carts).HasForeignKey(c => c.Customer_ID);
			modelBuilder.Entity<Cart>().HasOne(c => c.Book).WithMany(c => c.Carts).HasForeignKey(c => c.ISBN);

			modelBuilder.Entity<Order>().HasOne(c => c.Book).WithMany(c => c.Orders).HasForeignKey(c => c.BookId);
			modelBuilder.Entity<Order>().HasOne(c => c.Customer).WithMany(c => c.Orders).HasForeignKey(c => c.CustomerId);	

        }
    }
}