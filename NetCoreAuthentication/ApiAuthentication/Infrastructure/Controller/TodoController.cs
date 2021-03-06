namespace ApiAuthentication.Infrastructure.Controller
{
    using ApiAuthentication.Core.Entities;
    using ApiAuthentication.Core.Mapper;
    using ApiAuthentication.Infrastructure.Data;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TodoController: ApiBaseController<TodoRest, Todo>
    {
        public TodoController(AppDbContext ctx, TodoRepository todoRepository) : base(todoRepository, TodoMapper.INSTANCE)
        {

        }
    }
}
