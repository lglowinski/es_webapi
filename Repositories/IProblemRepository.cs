using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;
using MongoDB.Driver;

namespace ExpertalSystem.Repositories
{
    public interface IProblemRepository
    {
        Task<IEnumerable<Problem>> FindAsync(IssueType type);
        Task AddAsync(Problem entity);
        Task<IEnumerable<Problem>> FindAsync(Expression<Func<Problem, bool>> expression);
        Task<IEnumerable<Problem>> FindAsync();
        Task<Problem> GetAsync(Guid id);
        Task<Problem> GetAsync(Expression<Func<Problem, bool>> expression);
    }
}
