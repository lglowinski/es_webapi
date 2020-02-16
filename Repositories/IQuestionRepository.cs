using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpertalSystem.Domain;

namespace ExpertalSystem.Repositories
{
    public interface IQuestionRepository
    {
        Task<List<string>> GetQuestionAnswers(Guid id);
        Task<List<string>> GetQuestionAnswers(string questionName);
        Task AddAsync(Question entity);
        Task<IEnumerable<Question>> FindAsync(Expression<Func<Question, bool>> expression);
        Task<IEnumerable<Question>> FindAsync();
        Task<Question> GetAsync(Guid id);
        Task<Question> GetAsync(Expression<Func<Question, bool>> expression);
        Task UpdateAsync(Question entity);
    }
}
