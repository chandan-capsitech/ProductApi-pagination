using MongoDB.Driver;
using ProductApi.Data;
using ProductApi.Models; 

namespace ProductApi.Services;
public class ProductService
{
    private readonly IMongoCollection<Product> _products;
    public ProductService(MongoDbContext context)
    {
        _products = context.Products;
    }

    //Hey this is dwaipayan

    public async Task<PaginationResponse<Product>> GetProductsAsync(int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 3;

        var skip = (page - 1) * pageSize;
        var totalItems = await _products.CountDocumentsAsync(FilterDefinition<Product>.Empty);

        var products = await _products.Find(FilterDefinition<Product>.Empty)
            .SortBy(p => p.Id)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync();

        return new PaginationResponse<Product>
        {
            Data = products,
            TotalItems = (int)totalItems
        };
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await _products.InsertOneAsync(product);
        return product;
    }
}