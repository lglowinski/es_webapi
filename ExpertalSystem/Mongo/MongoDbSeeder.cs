using MongoDB.Driver;

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
