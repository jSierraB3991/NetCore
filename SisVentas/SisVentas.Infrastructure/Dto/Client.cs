namespace SisVentas.Infrastructure.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("client")]
    public class Client: BaseEntity
    {
        public string Name { get; set; }
    }
}
