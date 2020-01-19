using ExpertalSystem.Dtos;
using System.Threading.Tasks;

namespace ExpertalSystem.LogicServices
{
    public interface ILogicService
    {
        Task<Question> CalculateQuestion(string questionBefore, string answere);

        Task<Response> CalculateConclusion();
    }
}
