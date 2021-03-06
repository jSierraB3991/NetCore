namespace ApiAuthentication.Infrastructure.Data
{
    using ApiAuthentication.Core.Entities;
    using ApiAuthentication.Shared.Utils;
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserRepository
    {
        private readonly AppDbContext ctx;

        public UserRepository(AppDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<bool> Register(User user)
        {
            if (!ctx.Users.Any(u => u.UserName.Equals(user.UserName)))
            {
                user.Salt = Convert.ToBase64String(Encrypted.GetRandomSalt(16));
                user.Password = Convert.ToBase64String(
                    Encrypted.SaltHashPassword(
                        Encoding.ASCII.GetBytes(user.Password),
                        Convert.FromBase64String(user.Salt)));
                ctx.Set<User>().Add(user);
                await ctx.SaveChangesAsync();
                return false;
            }
            return true;
        }


        public bool Login(User user)
        {
            if (ctx.Users.Any(u => u.UserName.Equals(user.UserName)))
            {
                User userdto = ctx.Users.Where(u => u.UserName.Equals(user.UserName)).First();
                var client_post_hash_password = Convert.ToBase64String(Encrypted.SaltHashPassword(
                    Encoding.ASCII.GetBytes(user.Password), Convert.FromBase64String(userdto.Salt)));
                if(client_post_hash_password.Equals(userdto.Password))
                    return true;
                return false;
            }
            return false;
        }
    }
}
