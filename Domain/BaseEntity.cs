using ExpertalSystem.Mongo;
using MongoDB.Bson;

namespace ExpertalSystem.Domain
{
    public class BaseEntity : IBase
    {
        public string Name { get; }
    }
}
