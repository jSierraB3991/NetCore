namespace ApiAuthentication.Core.Mapper
{
    using ApiAuthentication.Core.Entities;
    using Common;

    public class UserMapper : GenericMapper<UserRest, User>
    {
        public static UserMapper INSTANCE = new UserMapper();

        public override User GetEntity(UserRest rest)
        {
            return new User()
            {
                Id = rest.Id,
                Password = rest.Password,
                Salt = rest.Salt,
                UserName = rest.UserName
            };
        }

        public override UserRest GetRest(User entity)
        {
            return new UserRest()
            {
                Id = entity.Id,
                Password = entity.Password,
                Salt = entity.Salt,
                UserName = entity.UserName
            };
        }
    }
}
