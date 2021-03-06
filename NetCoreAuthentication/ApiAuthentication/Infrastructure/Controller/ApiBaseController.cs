using ApiAuthentication.Core.Mapper;
using ApiAuthentication.Shared;
using ApiAuthentication.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiAuthentication.Infrastructure.Controller
{
    public class ApiBaseController<R, M> : ControllerBase where M : BaseEntity
    {
        private IRepository<M> repository;
        private readonly GenericMapper<R, M> mapper;

        public ApiBaseController(IRepository<M> repository, GenericMapper<R, M> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<R>> Get()
        {
            return mapper.GetListRest(await repository.GetListAsync());
        }

        [HttpGet("{id}")]
        public virtual async Task<R> Get(int id)
        {
            return mapper.GetRest(await repository.GetByIdAsync(id));
        }

        [HttpPost]
        public virtual async Task<R> Post([FromBody] R rest)
        {
            M model = await repository.AddAsync(mapper.GetEntity(rest));
            return mapper.GetRest(model);
        }

        [HttpPut]
        public virtual async Task<R> Put([FromBody] R rest)
        {
            M model = await repository.UpdateAsync(mapper.GetEntity(rest));
            return mapper.GetRest(model);
        }

        [HttpDelete("{id}")]
        public virtual async Task Delete(int id)
        {
            await repository.DeleteAsync(await repository.GetByIdAsync(id));
        }
    }
}
