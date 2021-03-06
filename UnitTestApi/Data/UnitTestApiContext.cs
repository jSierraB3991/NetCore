using Microsoft.EntityFrameworkCore;

namespace UnitTestApi.Data
{
    public class UnitTestApiContext : DbContext
    {
        public UnitTestApiContext(DbContextOptions<UnitTestApiContext> options)
            : base(options)
        {
        }

        public DbSet<UnitTestApi.Models.Book> Book { get; set; }
    }
}
