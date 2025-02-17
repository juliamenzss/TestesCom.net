namespace TaNaLista.Domain.Models
{
    public class ShoppingListProduct
    {
        public long ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public long ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; } = default!;

    }
}
