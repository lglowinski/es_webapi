﻿using MongoDB.Bson;
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
        private readonly IMongoCollection<TEntity> _mongoCollection;
        public Repository(IMongoDatabase mongoDatabase, string collection)
        {
            _mongoCollection = mongoDatabase.GetCollection<TEntity>(collection);
        }

        public async Task AddAsync(TEntity entity)
            => await _mongoCollection.InsertOneAsync(entity, new InsertOneOptions());

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression) => await _mongoCollection.Find(expression).ToListAsync();
        public async Task<IEnumerable<TEntity>> FindAsync() => await _mongoCollection.Find(p=>p!=null).ToListAsync();

        public async Task<TEntity> GetAsync(Guid id)
        => await _mongoCollection.Find(p => p.Id.Equals(id)).SingleOrDefaultAsync();

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        => await _mongoCollection.Find(expression).SingleOrDefaultAsync();
    }
}
