using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpertalSystem.Mongo
{
    public interface IRepository<TEntity> where TEntity : IBase
    {
        Task<TEntity> GetAsync(ObjectId id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);

        Task AddAsync(TEntity entity);
    }
}
