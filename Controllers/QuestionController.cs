using ExpertalSystem.Domain;
using ExpertalSystem.Dtos;
using ExpertalSystem.Repositories;
using ExpertalSystem.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionController : BaseController
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IClausesRepository _clausesRepository;
        public QuestionController(IQuestionRepository questionRepository,
            IClausesRepository clausesRepository)
        {
            _questionRepository = questionRepository;
            _clausesRepository = clausesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Question>>> GetAllQuestion([FromQuery] GetAllQuestionsRequest request)
        {
            var questions = await _questionRepository.FindAsync(p=>p.IssueTypes == request.IssueType);
            return Ok(questions);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> CreateQuestion([FromBody] CreateQuestionRequest request)
        {
            if (await _questionRepository.GetAsync(p => p.RuleName.Equals(request.RuleName)) != null)
                return Conflict("Rule with this name already exists in database");

            var newQuestion = new Domain.Question()
            {
                Id = Guid.NewGuid(),
                RuleName = request.RuleName,
                Consequence = request.Consequence,
                IssueTypes = request.IssueType
            };

            foreach(var clause in request.ClausesFields)
            {
                var newClause = new Clause()
                {
                    Id = Guid.NewGuid(),
                    ClauseName = clause.Clause
                };

                var dbClause = await _clausesRepository.GetAsync(p => p.ClauseName.Equals(clause.Clause));

                if (dbClause != null)
                {
                    newQuestion.Clauses.Add(new ClausesBasic
                    {
                        Id = dbClause.Id,
                        Answer = clause.Answer
                    });
                }
                else
                {
                    await _clausesRepository.AddAsync(newClause);
                    newQuestion.Clauses.Add(new ClausesBasic
                    {
                        Id = newClause.Id,
                        Answer = clause.Answer
                    });
                }
            }
            await _questionRepository.AddAsync(newQuestion);
            return Created($"{Request.Path.Value}/{newQuestion.Id}", newQuestion);
        }

    }
}
