namespace ApiAuthentication.Core.Mapper
{
    using ApiAuthentication.Core.Entities;
    using Common;

    public class TodoMapper : GenericMapper<TodoRest, Todo>
    {
        public static TodoMapper INSTANCE = new TodoMapper();

        public override Todo GetEntity(TodoRest rest)
        {
            return new Todo() {
                Description = rest.Description,
                Id = rest.Id,
                IsDone = rest.IsDone,
                Title = rest.Title
            };
        }

        public override TodoRest GetRest(Todo entity)
        {
            return new TodoRest() {
                Description = entity.Description,
                Title = entity.Title,
                IsDone = entity.IsDone,
                Id = entity.Id
            };
        }
    }
}
