using Microsoft.EntityFrameworkCore;
using TaNaLista.Interfaces;
using TaNaLista.Response;

namespace TaNaLista.Services
{
    public class ProductService(IProductContext context) : IProductService
    {
        public readonly IProductContext _context = context;


        public async Task<IEnumerable<ProductResponse>> GetAll(int page = 1, int pageSize = 10)
        {
            var product = await _context.Products
                .OrderBy(x => x.Id)
                .Select(x => new ProductResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                })
            .Skip((page -1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            return product;
        }

        public async Task<int> GetTotal()
        {
            return await _context.Products.CountAsync();
        }

    }
}
