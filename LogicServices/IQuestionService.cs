using ExpertalSystem.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpertalSystem.LogicServices
{
    public interface IQuestionService
    {
        Question GetFirstQuestion();
        Question GetNextQuestion(Question previouseQuestion, string answare);
    }
}
