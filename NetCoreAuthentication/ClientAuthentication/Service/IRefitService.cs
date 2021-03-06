namespace ClientAuthentication.Service
{
    using Common;
    using Refit;
    using System.Threading.Tasks;

    interface IRefitService
    {
        [Post("/api/user/register")]
        Task<string> RegisterUser([Body] UserRest user);

        [Post("/api/user/login")]
        Task<string> Login([Body] UserRest user);
    }
}