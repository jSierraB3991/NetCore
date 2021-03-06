using ApiAuthentication.Shared;
using System.ComponentModel.DataAnnotations;

namespace ApiAuthentication.Core.Entities
{
    public class User: BaseEntity
    {
        [Required(ErrorMessage = "The UserName in User is required")]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
    }
}
