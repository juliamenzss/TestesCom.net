using Microsoft.EntityFrameworkCore;
using TaNaLista.Communication.Requests;
using TaNaLista.Communication.Response;
using TaNaLista.Domain.Interfaces;
using TaNaLista.Domain.Models;

namespace TaNaLista.Application.Services
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
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            return product;
        }

        public async Task<int> GetTotal()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<Product> Create(ProductCreateRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }
    }
}
