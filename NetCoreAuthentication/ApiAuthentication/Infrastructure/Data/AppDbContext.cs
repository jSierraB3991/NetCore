namespace ApiAuthentication.Infrastructure.Data
{
    using ApiAuthentication.Core.Entities;
    using ApiAuthentication.Shared;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options): base(options: options)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var AddedEntities = ChangeTracker.Entries<BaseEntity>().Where(E => E.State == EntityState.Added).ToList();
            AddedEntities.ForEach(E =>
            {
                E.Entity.CreationDate = DateTime.Now;
                E.Entity.ModifyDate = DateTime.Now;
            });

            var ModifiedEntities = ChangeTracker.Entries<BaseEntity>().Where(E => E.State == EntityState.Modified).ToList();
            ModifiedEntities.ForEach(E =>
            {
                E.Entity.ModifyDate = DateTime.Now;
            });
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
