using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpertalSystem.Repositories
{
    public class ClausesRepository : IClausesRepository
    {
        private readonly IRepository<Clause> _repository;
        public ClausesRepository(IRepository<Clause> repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(Clause entity)
            => await _repository.AddAsync(entity);

        public async Task<IEnumerable<Clause>> FindAsync(Expression<Func<Clause, bool>> expression)
            => await _repository.FindAsync(p => p != null);

        public async Task<IEnumerable<Clause>> FindAsync()
            => await _repository.FindAsync();

        public async Task<Clause> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<Clause> GetAsync(Expression<Func<Clause, bool>> expression)
            => await _repository.GetAsync(expression);
    }
}
