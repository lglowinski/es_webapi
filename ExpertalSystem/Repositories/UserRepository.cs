using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;

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
            => await _repository.AddAsync(entity);

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> expression)
            => await _repository.FindAsync(p => p != null);

        public async Task<IEnumerable<User>> FindAsync()
            => await _repository.FindAsync();

        public async Task<User> GetAsync(string login, string password)
            => await _repository.GetAsync(x=>x.Name.Equals(login) && x.Password.Equals(password));
        
        public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
            => await _repository.GetAsync(expression);

        public async Task<User> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task UpdateAsync(User entity)
            => await _repository.UpdateAsync(entity);
    }
}
