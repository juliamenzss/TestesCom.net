using Microsoft.EntityFrameworkCore;
using TaNaLista.Domain.Models;

namespace TaNaLista.Domain.Interfaces
{
    public interface IUserContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
