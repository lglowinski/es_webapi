using Autofac;
using ExpertalSystem.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Mongo
{
    public static class MongoDbSeeder 
    {
        public static void Seed<TEntity>(IMongoDatabase db, string collection) where TEntity : IBase
        {
            if (db.GetCollection<TEntity>(collection) == null)
                db.CreateCollection(collection);
        }
    }
}
