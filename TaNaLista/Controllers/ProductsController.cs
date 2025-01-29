using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TaNaLista.Data;
using TaNaLista.Interfaces;
using TaNaLista.Models;
using TaNaLista.Response;

namespace TaNaLista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<Product> _logger;


        public ProductsController(IProductService service, ILogger<Product> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [SwaggerOperation(Summary = "Return a list of Products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> GetProducts(int page = 1, int pagesize = 10)
        {
            var products = await _service.GetAll(page, pagesize);

            if(products == null || !products.Any())
            {
                return NotFound("Products not found");
            }
            var result = new PaginateResultResponse<ProductResponse>
            {
                CurrentPage = page,
                PageSize = pagesize,
                TotalItems = await _service.GetTotal(),
                Items = products
            };

            return Ok(result);
        }


        //    [ProducesResponseType<Product>(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    [HttpGet("{id}")]
        //    public async Task<IActionResult> GetProduct(int id)
        //    {
        //        var product = await context.Products
        //            .Where(x => x.Id == id)
        //            .Select(x => new
        //            {
        //                x.Id,
        //                x.Name,
        //                x.Price,
        //                x.Description,
        //                ShoppingList = x.ShoppingListProducts.Count
        //            })
        //            .SingleOrDefaultAsync();

        //        if (product == null)
        //        {
        //            logger.LogWarning("Product is null");
        //            return NotFound();
        //        }

        //        return Ok(product);
        //    }


        //    [HttpPut("{id}")]
        //    [ProducesResponseType(StatusCodes.Status204NoContent)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public async Task<IActionResult> PutProduct(int id, Product product)
        //    {
        //        try
        //        {
        //            var productResult = await context.Products
        //                .SingleOrDefaultAsync(x => x.Id == id);

        //            if (productResult == null)
        //            {
        //                logger.LogWarning("Product is null");
        //                return NotFound();
        //            }

        //            productResult.Name = product.Name;
        //            productResult.Price = Math.Round(product.Price, 2);

        //            await context.SaveChangesAsync();
        //            return Ok(productResult);

        //        }
        //        catch (DbException dbEx)
        //        {
        //            logger.LogError(dbEx, "Error in database");
        //            return BadRequest();
        //        }

        //        catch (Exception ex)
        //        {
        //            logger.LogError(ex, "Error update product");
        //            return BadRequest();
        //        }
        //    }


        //    [HttpPost]
        //    [ProducesResponseType(StatusCodes.Status201Created)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    public async Task<IActionResult> PostProduct(Product product)
        //    {
        //        try
        //        {
        //            var newProduct = new Product
        //            {
        //                Name = product.Name,
        //                Description = product.Description,
        //                Price = Math.Round(product.Price, 2)

        //            };
        //            context.Products.Add(product);
        //            await context.SaveChangesAsync();

        //            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        //        }

        //        catch (DbException dbEx)
        //        {
        //            logger.LogError(dbEx, "Error in database");
        //            return BadRequest();
        //        }

        //        catch (Exception ex)
        //        {
        //            logger.LogError(ex, "Error create product");
        //            return BadRequest();
        //        }
        //    }



        //    [HttpDelete("{id}")]
        //    [ProducesResponseType(StatusCodes.Status204NoContent)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public async Task<IActionResult> DeleteProduct(int id)
        //    {

        //        await context.Products.Where(x => x.Id == id).ExecuteDeleteAsync();
        //        return NoContent();
        //    }
    }
}