﻿namespace TaNaLista.Communication.Response
{
    public class ProductResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
