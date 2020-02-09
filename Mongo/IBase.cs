using ExpertalSystem.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Mongo
{
    public interface IBase
    {
        public ObjectId Id { get; set; }
        string Name { get; set; }
    }
}
