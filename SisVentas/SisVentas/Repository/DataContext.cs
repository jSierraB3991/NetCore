namespace SisVentas.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SisVentas.Infrastructure.Dto;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Client> Clients { get; set; }
    }
}