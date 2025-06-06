﻿namespace TaNaLista.Communication.Response
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ShoppingLists { get; set; }
    }
}
