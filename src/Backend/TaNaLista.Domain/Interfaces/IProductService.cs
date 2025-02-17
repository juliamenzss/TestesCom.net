using TaNaLista.Communication.Requests;
using TaNaLista.Communication.Response;
using TaNaLista.Domain.Models;

namespace TaNaLista.Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAll(int page = 1, int pageSize = 10);
        Task<int> GetTotal();
        Task<Product> Create(ProductCreateRequest request);
    }
}
