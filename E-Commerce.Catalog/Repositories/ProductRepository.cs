using Catalog.Repositories;
using E_Commerce.Catalog.Data;
using E_Commerce.Catalog.Entities;
using MongoDB.Driver;

namespace E_Commerce.Catalog.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }

    public async Task CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var result = await _context.Products.DeleteOneAsync(filter);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _context
            .Products.Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
        return await _context
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
        return await _context
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context
            .Products.Find(p => true)
            .ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await _context.Products.ReplaceOneAsync(g => g.Id == product.Id, product);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}