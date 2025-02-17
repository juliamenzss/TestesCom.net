namespace TaNaLista.Domain.Models
{
    public class Product : ModelBase
    {
        public string Name { get; set; } = string.Empty!;
        public string Description { get; set; } = string.Empty!;
        public decimal Price { get; set; } = default!;
        public ICollection<ShoppingListProduct> ShoppingListProduct { get; set; } = [];
    }
}