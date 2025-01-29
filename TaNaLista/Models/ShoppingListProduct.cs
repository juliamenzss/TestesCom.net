namespace TaNaLista.Models
{
    public class ShoppingListProduct
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public Guid ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; } = default!;

    }
}
