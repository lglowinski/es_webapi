using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

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
        Task<DeleteResult> DeleteAsync(Guid id);
        Task UpdateManyAsync(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update);
    }
}
