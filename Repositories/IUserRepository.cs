using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;

namespace ExpertalSystem.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User entity);
        Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> FindAsync();
        Task<User> GetAsync(string login, string password);
        Task<User> GetAsync(Expression<Func<User, bool>> expression);
        Task<User> GetAsync(Guid id);
        Task UpdateAsync(User entity);
    }
}
