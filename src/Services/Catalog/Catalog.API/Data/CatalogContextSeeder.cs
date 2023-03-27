using Catalog.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeeder
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreConfigurationProducts());
            }

        }

        private static IEnumerable<Product> GetPreConfigurationProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Category = "App1",
                    Description= "App Description1",
                    Id = ObjectId.GenerateNewId().ToString(),
                    ImageFile = "1",
                    Name = "1",
                    Price = 105001,
                    Summary = "1"

                },
                new Product()
                {
                    Category = "App2",
                    Description= "App Description2",
                    Id = ObjectId.GenerateNewId().ToString(),
                    ImageFile = "2",
                    Name = "2",
                    Price = 105002,
                    Summary = "2"

                },
                new Product()
                {
                    Category = "App3",
                    Description= "App Description3",
                    Id = ObjectId.GenerateNewId().ToString(),
                    ImageFile = "3",
                    Name = "3",
                    Price = 105003,
                    Summary = "3"

                }
            };
        }
    }
}
    
