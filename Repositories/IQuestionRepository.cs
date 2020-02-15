using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpertalSystem.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        public Task<List<string>> GetQuestionAnswers(Guid id);
        public Task<List<string>> GetQuestionAnswers(string questionName);
    }
}
