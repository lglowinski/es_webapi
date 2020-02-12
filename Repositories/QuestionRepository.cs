using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpertalSystem.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IRepository<Question> _repository;

        public QuestionRepository(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Question entity)
            => await _repository.AddAsync(entity);

        /// <summary>
        /// Returns all questions
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Question>> FindAsync(Expression<Func<Question, bool>> expression)
            => await _repository.FindAsync(expression);

        /// <summary>
        /// Returns questions by given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Question>> FindAsync(IssueTypes type)
            => await _repository.FindAsync(p => p.IssueTypes == type);

        public async Task<IEnumerable<Question>> FindAsync()
            => await _repository.FindAsync();

        public async Task<Question> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<Question> GetAsync(Expression<Func<Question, bool>> expression)
            => await _repository.GetAsync(expression);
    }
}
