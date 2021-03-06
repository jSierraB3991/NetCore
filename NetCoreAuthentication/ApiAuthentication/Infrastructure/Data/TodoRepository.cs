namespace ApiAuthentication.Infrastructure.Data
{
    using ApiAuthentication.Core.Entities;

    public class TodoRepository : EfRepository<Todo>
    {
        public TodoRepository(AppDbContext ctx): base(ctx, "TODO")
        {

        }
    }
}
