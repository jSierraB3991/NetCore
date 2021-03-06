namespace SisVentas.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using SisVentas.Infrastructure.Dto;
    using SisVentas.Infrastructure.Rest;
    using SisVentas.Localization;
    using SisVentas.Repository;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IStringLocalizer<SharedResource> stringLocalizer;

        public ClientController(DataContext dataContext, IStringLocalizer<SharedResource> stringLocalizer)
        {
            this.dataContext = dataContext;
            this.stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var clients = dataContext.Clients.ToList().Select( c => 
                    new ClientRest() { 
                        Id = c.Id,
                        Name = c.Name
                    }
                ).OrderByDescending(c => c.Id).ToList();

            return Ok(new Response<List<ClientRest>>() {
                Data = clients,
                IsSuccess = 1,
                Message = string.Empty
            }); 
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var clientDto = dataContext.Clients.Find(id);
            if (clientDto == null) {
                return BadRequest(new Response<ClientRest>()
                {
                    IsSuccess = 0,
                    Message = stringLocalizer["ClientNotFound"]
                });
            }
            var client = new ClientRest() { Id = clientDto.Id, Name = clientDto.Name };
            return Ok(new Response<ClientRest>()
            {
                Data = client,
                IsSuccess = 1,
                Message = string.Empty
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientRest client)
        {
            var clientDto = new Client {
                Id = 0,
                Name = client.Name
            };

            this.dataContext.Clients.Add(clientDto);
            await dataContext.SaveChangesAsync();
            client.Id = clientDto.Id;
            return Ok(new Response<ClientRest>()
            {
                IsSuccess = 1,
                Message = string.Empty,
                Data = client
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClientRest client)
        {
            client.Id = id;
            var clientDto = new Client  {
                Id = id,
                Name = client.Name
            };
            this.dataContext.Clients.Update(clientDto);
            await dataContext.SaveChangesAsync();
            return Ok(new Response<ClientRest>()
            {
                IsSuccess = 1,
                Message = string.Empty,
                Data = client
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Client client = dataContext.Clients.Find(id);
            if (client == null)
            {
                return BadRequest(new Response<ClientRest>() {
                    IsSuccess = 0,
                    Message = stringLocalizer["ClientNotFound"]
                });
            }
            this.dataContext.Clients.Remove(client);
            await dataContext.SaveChangesAsync();
            return Ok(new Response<ClientRest>()
            {
                IsSuccess = 1,
                Message = stringLocalizer["DeleteCliente"]
            });
        }
    }
}
