using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpertalSystem.Caching;
using ExpertalSystem.Domain;
using ExpertalSystem.Dtos;
using ExpertalSystem.Repositories;
using ExpertalSystem.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpertalSystem.Controllers
{
    [Route("problems")]
    public class ProblemController : BaseController
    {
        private readonly IProblemRepository _problemRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ICacheProvider _cacheProvider;
        public ProblemController(IProblemRepository problemRepository,
            IQuestionRepository questionRepository,
            ICacheProvider cacheProvider)
        {
            _problemRepository = problemRepository;
            _questionRepository = questionRepository;
            _cacheProvider = cacheProvider;
        }

        [HttpGet]
        [Cached]
        public async Task<ActionResult<IEnumerable<Domain.Problem>>> GetAllProblems([FromQuery] GetAllProblemsRequest request)
        {
            var problems = await _problemRepository.FindAsync(p=>p.IssueType == request.IssueType);
            return Ok(problems);
        }

        [HttpGet("problemNames")]
        [Cached]
        public async Task<ActionResult<IEnumerable<ProblemNameResponse>>> GetProblemsNames([FromQuery] GetAllProblemsRequest request)
        {
            var response = new List<ProblemNameResponse>();
            var problems = await _problemRepository.FindAsync(p => p.IssueType == request.IssueType);
            var problemsLite = from problem in problems select new {problem.Id, problem.ProblemName};
            foreach (var problem in problemsLite)
            {
                response.Add(new ProblemNameResponse
                {
                    Id = problem.Id,
                    ProblemName = problem.ProblemName
                });
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Cached]
        public async Task<ActionResult<IEnumerable<Domain.Problem>>> GetProblem([FromRoute] Guid id)
        {
            var problem = await _problemRepository.GetAsync(id);
            return Ok(problem);
        }

        /// <summary>
        /// Add new problem to database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Question>> CreateProblem([FromBody] CreateProblemRequest request)
        {
            if (await _problemRepository.GetAsync(p => p.ProblemName.Equals(request.ProblemName)) != null)
                return Conflict("Rule with this name already exists in database");

            var newProblem = new Domain.Problem()
            {
                Id = Guid.NewGuid(),
                ProblemName = request.ProblemName,
                Solution = request.Solution,
                IssueType = request.IssueType
            };

            foreach (var question in request.Questions)
                newProblem.Questions.Add(await GetQuestion(question));
            

            await _problemRepository.AddAsync(newProblem);
            await _cacheProvider.DumpCache();
            return Created($"{Request.Path.Value}/{newProblem.Id}", newProblem);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProblem([FromRoute]Guid id)
        {
            var result = await _problemRepository.DeleteAsync(id);
            if (result.DeletedCount <= 0) return NotFound();

            await _cacheProvider.DumpCache();
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProblem([FromRoute]Guid id, [FromBody]UpdateProblemRequest request)
        {
            var fetchedProblem = await _problemRepository.GetAsync(id);

            if (fetchedProblem is null)
                return NotFound("Nie znaleziono problemu do edycji");

            var updatedQuestionList = new List<QuestionBasic>();

            foreach (var question in request.Questions)
                updatedQuestionList.Add(await GetQuestion(question));

            fetchedProblem.Questions = updatedQuestionList;
            fetchedProblem.Solution = request.Solution;
            fetchedProblem.ProblemName = request.ProblemName;

            await _problemRepository.UpdateAsync(fetchedProblem);
            await _cacheProvider.DumpCache();
            return Accepted();
        }

        private List<string> DefaultAnswers = new List<string>()
        {
            "tak",
            "nie"
        };

        private async Task<QuestionBasic> GetQuestion(QuestionRequest question)
        {
            var newQuestion = new Question()
            {
                Id = Guid.NewGuid(),
                Answers = new List<string>(),
                QuestionName = question.QuestionName
            };

            if (!DefaultAnswers.Any(defaultAnswer=> defaultAnswer.Equals(question.Answer)))
            {
                newQuestion.Answers.AddRange(DefaultAnswers);
                newQuestion.Answers.Add(question.Answer);
            }
            else
            {
                newQuestion.Answers.AddRange(DefaultAnswers);
            }

            

            var dbQuestion = await _questionRepository.GetAsync(p => p.QuestionName.Equals(question.QuestionName));

            if (dbQuestion != null)
            {
                if (!dbQuestion.Answers.Contains(question.Answer))
                {
                    dbQuestion.Answers.Add(question.Answer);
                    await _questionRepository.UpdateAsync(dbQuestion);
                }

                return new QuestionBasic
                {
                    Id = dbQuestion.Id,
                    Answer = question.Answer,
                    QuestionName = dbQuestion.QuestionName
                };
            }

            await _questionRepository.AddAsync(newQuestion);
            return new QuestionBasic
            {
                Id = newQuestion.Id,
                Answer = question.Answer,
                QuestionName = newQuestion.QuestionName
            };
        }
    }
}
