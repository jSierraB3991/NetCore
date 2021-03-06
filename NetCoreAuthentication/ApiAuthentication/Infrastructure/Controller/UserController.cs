namespace ApiAuthentication.Infrastructure.Controller
{
    using ApiAuthentication.Core.Mapper;
    using ApiAuthentication.Infrastructure.Data;
    using Common;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly UserRepository repository;

        public UserController(UserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<string> Register(UserRest user)
        {
            var existsUser = await repository.Register(UserMapper.INSTANCE.GetEntity(user));
            if (existsUser) return JsonConvert.SerializeObject("User in the Database");

            return JsonConvert.SerializeObject("Save User In Database Sucessfull");
        }

        [HttpPost]
        [Route("login")]
        public string Login(UserRest user)
        {
            var existsUser = repository.Login(UserMapper.INSTANCE.GetEntity(user));

            if (!existsUser) return JsonConvert.SerializeObject("User Or Password Incorrect");

            user.Password = string.Empty;
            return JsonConvert.SerializeObject(user);
        }

    }
}
