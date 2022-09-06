using System.Net;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    private readonly IProductRepository _repository;

    public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var res = await _repository.GetProducts();
        return Ok(res);
    }

    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        var product = await _repository.GetProduct(id);
        if (product == null)
        {
            _logger.LogError($"Product with id :{id},not found");
            return NotFound();
        }

        return Ok(product);
    }

    [Route("[action]/{category}", Name = "GetProductByCategory")]
    [HttpPost]
    [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        await _repository.CreateProduct(product);
        return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        return Ok(await _repository.UpdateProduct(product));
    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id)
    {
        return Ok(await _repository.DeleteProduct(id));
    }
}