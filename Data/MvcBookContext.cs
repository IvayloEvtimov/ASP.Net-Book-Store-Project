using Microsoft.EntityFrameworkCore;
using Project.Models;


namespace Project.Data
{
    public class MvcBookContext : DbContext
    {
        public MvcBookContext(DbContextOptions<MvcBookContext> options):base(options)
        {

        }

        public DbSet<Book> Book { get; set; }
    }
}