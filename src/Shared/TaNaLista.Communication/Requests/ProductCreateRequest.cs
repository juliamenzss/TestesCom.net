using System.ComponentModel.DataAnnotations;

namespace TaNaLista.Communication.Requests
{
    public class ProductCreateRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2), MaxLength(15)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [RegularExpression(@"^\d+(\.\d{2})$", ErrorMessage = "Price must be have exactly two decimal places")]
        [Range(1.00, double.MaxValue, ErrorMessage = "Price must be at least 1.00.")]
        public decimal Price { get; set; }
    }
}
