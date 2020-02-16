using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using chen0040.ExpertSystem;
using ExpertalSystem.Repositories;
using ExpertalSystem.Requests;
using Microsoft.Extensions.Logging;

namespace ExpertalSystem.LogicServices
{
    public class QuestionService : IQuestionService
    {
        private readonly IEngineGenerator _generator;
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<QuestionService> _logger;

        public QuestionService(IEngineGenerator generator, IQuestionRepository questionRepository, ILogger<QuestionService> logger)
        {
            _generator = generator;
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<object> Conclude(ConcludeRequest request)
        {

            var engine = await _generator.CreateEngine(request.Type);
            Clause currentClause = null;
            List<string> answersForCurrentQuestions = null;
            var unprovedConditions = new List<Clause>();

            if (request.Facts != null) {
                foreach (IncomingClause clause in request.Facts)
                {
                    var fact = new IsClause(clause.Variable, clause.Value);
                    engine.AddFact(fact);
                } 
            }
            if (request.Answer != null)
                engine.AddFact(new IsClause(request.PreviousQuestion, request.Answer));

            var conclusion = engine.Infer("Solution", unprovedConditions);

            if (conclusion is null)
            {
                try
                {
                    currentClause = unprovedConditions[0];
                    answersForCurrentQuestions = await _questionRepository.GetQuestionAnswers(currentClause.Variable);
                }
                catch(ArgumentOutOfRangeException exception)
                {
                    _logger.LogError(exception.Message);
                    return new { error = $"No problem found for issue type: {request.Type}" };
                }
            }

            if (conclusion != null) return new
            {
                question = "",
                answers = new List<string>(),
                facts = "",
                consequence = $"{conclusion.Value}:{conclusion.Value}"
            };

            return new
            {
                question = currentClause.Variable,
                answers = answersForCurrentQuestions,
                facts = engine.Accessor.GetFacts(engine.Facts),
                consequence = ""
            };
        }
    }
}
