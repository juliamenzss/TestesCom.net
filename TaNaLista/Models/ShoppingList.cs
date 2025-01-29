namespace TaNaLista.Models
{
    public class ShoppingList
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public ICollection<ShoppingListProduct> ShoppingListProducts { get; set; } = [];
    }
}