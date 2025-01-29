namespace TaNaLista.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public int ShoppingLists { get; set; }
    }
}
