using CatalogSpa.API.Models;
using MongoDB.Driver;

namespace CatalogSpa.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase mongoDB;
        private readonly IMongoCollection<Product> productCollection;
        public ProductRepository(IMongoDatabase mongoDB) 
        {
            this.mongoDB = mongoDB;
            productCollection = mongoDB.GetCollection<Product> ("product");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await productCollection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            return await productCollection.Find(p => p.Category == category).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await productCollection.Find(p => p.Id == id).FirstOrDefaultAsync(); 
        }

        public async Task CreateProduct(Product product)
        {
           await productCollection.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result =
                await productCollection.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var result = await productCollection.DeleteOneAsync(filter: p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
