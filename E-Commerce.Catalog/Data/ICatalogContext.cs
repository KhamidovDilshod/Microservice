using E_Commerce.Catalog.Entities;
using MongoDB.Driver;

namespace E_Commerce.Catalog.Data;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
}