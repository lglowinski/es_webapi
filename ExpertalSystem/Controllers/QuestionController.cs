using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpertalSystem.Caching;
using ExpertalSystem.Domain;
using ExpertalSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpertalSystem.Controllers
{
    [Route("questions")]
    public class QuestionController : BaseController
    {
        private readonly IProblemRepository _problemRepository;
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IProblemRepository problemRepository, IQuestionRepository questionRepository)
        {
            _problemRepository = problemRepository;
            _questionRepository = questionRepository;
        }

        [HttpGet("{type}")]
        [Cached]
        public async Task<ActionResult<object>> GetQuestions([FromRoute] IssueType type)
        {
            var problems = (await _problemRepository.FindAsync(p => p.IssueType == type));
            List<string> distinctQuestionsNanmes = new List<string>();
            foreach (var problem in problems)
            {
                var questionsForProblem = problem.Questions.Select(p => p.QuestionName);
                foreach (var questionName in questionsForProblem.Select(p=>p).Distinct().ToList())
                {
                    distinctQuestionsNanmes.Add(questionName);
                }
            }
            var questionsListForType = new List<Question>();

            foreach(var questionName in distinctQuestionsNanmes)
            {
                var result = await _questionRepository.GetAsync(p=>p.QuestionName.Equals(questionName));
                if (questionsListForType.Any(p=>p.QuestionName.Equals(result.QuestionName))) continue;
                questionsListForType.Add(result);
            }

            return Ok(questionsListForType.Select(x=>x).Distinct().ToList());
        }
    }
}
