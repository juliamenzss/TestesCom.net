﻿using NPOI.SS.Formula.Functions;
using TaNaLista.Models;

namespace TaNaLista.Response
{
    public class PaginateResultResponse<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
