using ExpertalSystem.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace ExpertalSystem.Domain
{
    [BsonSerializer(typeof(ImpliedImplementationInterfaceSerializer<IBase, BaseEntity>))]
    public class BaseEntity : IBase
    {
        public string name { get; }
    }
}
