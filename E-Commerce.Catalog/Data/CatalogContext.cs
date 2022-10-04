using E_Commerce.Catalog.Entities;
using MongoDB.Driver;

namespace E_Commerce.Catalog.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        this.configuration = configuration;
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        CatalogContextSeed.SeedData(Products);
    }

    public IConfiguration configuration { get; }


    public IMongoCollection<Product> Products { get; }
}