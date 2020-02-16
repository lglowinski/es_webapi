using System.Threading.Tasks;
using ExpertalSystem.Dtos;
using ExpertalSystem.Requests;

namespace ExpertalSystem.LogicServices
{
    public interface IConcludingService
    {
        public Task<ConcludeResponse> Conclude(ConcludeRequest request);
    }
}
