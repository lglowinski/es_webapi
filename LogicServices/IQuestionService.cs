using ExpertalSystem.Dtos;
using ExpertalSystem.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpertalSystem.LogicServices
{
    public interface IQuestionService
    {
        public Task<object> Conclude(ConcludeRequest request);
    }
}
