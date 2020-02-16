using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;

namespace ExpertalSystem.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        public Task<List<string>> GetQuestionAnswers(Guid id);
        public Task<List<string>> GetQuestionAnswers(string questionName);
    }
}
