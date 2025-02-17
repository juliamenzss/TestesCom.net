using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaNaLista.Domain.Models
{
    public class User : ModelBase
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("last name")]
        public string LastName { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        public List<ShoppingList> ShoppingLists { get; set; } = default!;

    }
}
