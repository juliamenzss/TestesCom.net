using Microsoft.EntityFrameworkCore;
using TaNaLista.Domain.Models;

namespace TaNaLista.Domain.Interfaces
{
    public interface IProductContext
    {
        public DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}