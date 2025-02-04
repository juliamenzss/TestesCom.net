using TaNaLista.Models;
using TaNaLista.Requests;
using TaNaLista.Response;

namespace TaNaLista.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAll(int page = 1, int pageSize = 10);
        Task<int> GetTotal();
        Task<Product> Create(ProductCreateRequest request);
    }
}
