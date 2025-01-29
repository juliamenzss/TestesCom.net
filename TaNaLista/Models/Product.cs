namespace TaNaLista.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty!;
        public string Description { get; set; } = string.Empty!;
        public decimal Price { get; set; } = default!;
        public ICollection<ShoppingListProduct> ShoppingListProducts { get; set; } = [];
    }
}