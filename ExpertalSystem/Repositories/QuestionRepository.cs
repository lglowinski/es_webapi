using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;

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

        public async Task<IEnumerable<Question>> FindAsync(Expression<Func<Question, bool>> expression)
            => await _repository.FindAsync(p => p != null);

        public async Task<IEnumerable<Question>> FindAsync()
            => await _repository.FindAsync();

        public async Task<Question> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<Question> GetAsync(Expression<Func<Question, bool>> expression)
            => await _repository.GetAsync(expression);

        public async Task<List<string>> GetQuestionAnswers(Guid id)
            => (await _repository.GetAsync(p => p.Id == id)).Answers;

        public async Task<List<string>> GetQuestionAnswers(string questionName)
            => (await _repository.GetAsync(p => p.QuestionName.Equals(questionName))).Answers;

        public async Task UpdateAsync(Question entity)
            => await _repository.UpdateAsync(entity);
    }
}
