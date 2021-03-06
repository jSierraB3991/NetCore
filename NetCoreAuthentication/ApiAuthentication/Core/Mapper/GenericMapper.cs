namespace ApiAuthentication.Core.Mapper
{
    using ApiAuthentication.Shared;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class GenericMapper<Rest, Entity> where Entity: BaseEntity
    {
        public abstract Rest GetRest(Entity entity);

        public abstract Entity GetEntity(Rest rest);

        public List<Rest> GetListRest(List<Entity> entities) => entities.Select(e => GetRest(e)).ToList();

        public List<Entity> GetListEntities(List<Rest> rests) => rests.Select(r => GetEntity(r)).ToList();
    }
}
