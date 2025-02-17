using System.ComponentModel.DataAnnotations.Schema;

namespace TaNaLista.Domain.Models
{
    public class ModelBase
    {
        [Column("id")]
        public long Id { get; set; }
        
        [Column("active")]
        public bool Active { get; set; }

        [Column("create")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
