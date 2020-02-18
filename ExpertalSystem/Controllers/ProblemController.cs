using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using ExpertalSystem.Caching;
using ExpertalSystem.Domain;
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
            var problem = await _problemRepository.FindAsync(p=>p.IssueType == request.IssueType);
            return Ok(problem);
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
            {
                var newQuestion = new Question()
                {
                    Id = Guid.NewGuid(),
                    Answers = new List<string>()
                    {
                        "tak",
                        "nie"
                    },
                    QuestionName = question.QuestionName
                };

                var dbQuestion = await _questionRepository.GetAsync(p => p.QuestionName.Equals(question.QuestionName));

                if (dbQuestion != null)
                {
                    newProblem.Questions.Add(new QuestionBasic
                    {
                        Id = dbQuestion.Id,
                        Answer = question.Answer,
                        QuestionName = dbQuestion.QuestionName
                    });
                    if (dbQuestion.Answers.Contains(question.Answer)) continue;

                    dbQuestion.Answers.Add(question.Answer);
                    await _questionRepository.UpdateAsync(dbQuestion);
                }
                else
                {
                    await _questionRepository.AddAsync(newQuestion);
                    newProblem.Questions.Add(new QuestionBasic
                    {
                        Id = newQuestion.Id,
                        Answer = question.Answer,
                        QuestionName = newQuestion.QuestionName
                    });
                }
            }

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
        [HttpPut]
        public async Task<ActionResult> UpdateProblem([FromBody]UpdateProblemRequest request)
        {
            await _cacheProvider.DumpCache();
            return Accepted();
        }
    }
}
