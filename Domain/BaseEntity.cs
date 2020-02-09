using ExpertalSystem.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace ExpertalSystem.Domain
{
    public class BaseEntity : IBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
    }
}
