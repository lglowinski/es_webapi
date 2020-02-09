using ExpertalSystem.Authorization;
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
        public async Task AddAsync(User entity)
        {
            await _repository.AddAsync(entity);
        }

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string name)
        {
            return await _repository.GetAsync(name);
        }

        public async Task<User> GetAsync(string login, string password)
        {
            return await _repository.GetAsync(x=>x.Name.Equals(login) && x.Password.Equals(password));
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            return await _repository.GetAsync(expression);
        }
    }
}
