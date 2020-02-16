using System.Threading.Tasks;
using ExpertalSystem.Requests;

namespace ExpertalSystem.LogicServices
{
    public interface IQuestionService
    {
        public Task<object> Conclude(ConcludeRequest request);
    }
}
