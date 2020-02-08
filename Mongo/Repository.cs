using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpertalSystem.Mongo
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IBase
    {
        private IMongoCollection<TEntity> _mongoCollection;
        public Repository(IMongoDatabase mongoDatabase, string collection)
        {
            _mongoCollection = mongoDatabase.GetCollection<TEntity>(collection);
        }

        public async Task AddAsync(TEntity entity)
            => await _mongoCollection.InsertOneAsync(entity, new InsertOneOptions());


        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return (await _mongoCollection.FindAsync(expression)).Current.ToList();
        }

        public async Task<TEntity> GetAsync(string id)
        => await _mongoCollection.AsQueryable<TEntity>().SingleOrDefault(p => p.name.Equals(id));

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        => await _mongoCollection.AsQueryable<TEntity>().SingleOrDefault(expression);
    }
}
