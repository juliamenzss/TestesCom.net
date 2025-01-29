using Microsoft.EntityFrameworkCore;
using TaNaLista.Models;
using TaNaLista.Requests;
using TaNaLista.Response;

namespace TaNaLista.Interfaces
{
    public interface IUserContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
