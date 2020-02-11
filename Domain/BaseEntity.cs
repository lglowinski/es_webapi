using ExpertalSystem.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace ExpertalSystem.Domain
{
    public class BaseEntity : IBase
    {
        //[BsonRepresentation(BsonType)]
        public Guid Id { get; set; }
    }
}
