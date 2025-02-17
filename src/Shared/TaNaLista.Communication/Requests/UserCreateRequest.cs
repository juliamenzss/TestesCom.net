using System.ComponentModel.DataAnnotations;

namespace TaNaLista.Communication.Requests
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2), MaxLength(15)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(3), MaxLength(30)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be at least 8 and at max 20 characters long")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must have: 1 lowercase, 1 uppercase, 1 number, 1 non-alphanumeric character")]
        public string Password { get; set; } = string.Empty;

    }
}
