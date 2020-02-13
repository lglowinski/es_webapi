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
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> FindAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}
