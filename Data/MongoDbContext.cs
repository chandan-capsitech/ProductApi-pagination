using MongoDB.Driver;
using ProductApi.Models;

namespace ProductApi.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        public MongoDbContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetSection("MongoDbSettings:ConnectionString").Value);
            _database = client.GetDatabase(config.GetSection("MongoDbSettings:DatabaseName").Value);
            _collectionName = config.GetSection("MongoDbSettings:CollectionName").Value;
        }

        public IMongoCollection<Product> Products =>
            _database.GetCollection<Product>(_collectionName); 
    }
}