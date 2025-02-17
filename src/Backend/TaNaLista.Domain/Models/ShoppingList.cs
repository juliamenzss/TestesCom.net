namespace TaNaLista.Domain.Models
{
    public class ShoppingList : ModelBase
    {
        public string Name { get; set; } = string.Empty;
        public long UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public ICollection<ShoppingListProduct> ShoppingListProduct { get; set; } = [];
    }
}