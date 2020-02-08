using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpertalSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _repository;
        public UserRepository(IRepository<User> repository)
        {
            _repository = repository;
        }
        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
