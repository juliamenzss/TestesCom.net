using Microsoft.EntityFrameworkCore;
using TaNaLista.Models;

namespace TaNaLista.Interfaces
{
    public interface IProductContext
    {
        public DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}