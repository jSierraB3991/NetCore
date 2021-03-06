using ApiAuthentication.Shared;
using ApiAuthentication.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAuthentication.Infrastructure.Data
{
    public abstract class EfRepository<T> : IRepository<T>  where T : BaseEntity
    {
        private readonly AppDbContext ctx;
        private readonly string className;

        public EfRepository(AppDbContext ctx, string className)
        {
            this.ctx = ctx;
            this.className = className;
        }

        public virtual T SetSave(T entity) {
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            this.SetSave(entity);
            await ctx.Set<T>().AddAsync(entity);
            await ctx.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            ctx.Set<T>().Remove(entity);
            await ctx.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T result = await ctx.Set<T>().SingleOrDefaultAsync(e => e.Id.Equals(id));
            if (result == null)
                throw new System.Exception($"No se encontro el id del {className}");
            return result;

        }

        public Task<List<T>> GetListAsync()
        {
            return ctx.Set<T>().ToListAsync();
        }

        public virtual T SetUpdate(T entity) {
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this.SetUpdate(entity);
            await this.GetByIdAsync(entity.Id);
            ctx.Entry(entity).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
            return entity;
        }
    }
}
