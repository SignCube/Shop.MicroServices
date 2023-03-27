using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Principal;
using ZstdSharp.Unsafe;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement(nameof(Name))]
        public string? Name { get; set; }

        [BsonElement(nameof(Category))]
        public string? Category { get; set; }

        [BsonElement(nameof(Summary))]
        public string? Summary { get; set; }

        [BsonElement(nameof(Description))]
        public string? Description { get; set; }

        [BsonElement(nameof(ImageFile))]
        public string? ImageFile { get; set; }

        [BsonElement(nameof(Price))]
        public decimal Price { get; set; }

    }
}
