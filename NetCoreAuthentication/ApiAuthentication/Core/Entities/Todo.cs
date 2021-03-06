using ApiAuthentication.Shared;
using System.ComponentModel.DataAnnotations;

namespace ApiAuthentication.Core.Entities
{
    public class Todo : BaseEntity
    {
        [Required(ErrorMessage = "The Title in Todo is required")]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}
