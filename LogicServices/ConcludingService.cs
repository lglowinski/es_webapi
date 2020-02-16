using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chen0040.ExpertSystem;
using ExpertalSystem.Dtos;
using ExpertalSystem.Repositories;
using ExpertalSystem.Requests;
using Microsoft.Extensions.Logging;

namespace ExpertalSystem.LogicServices
{
    public class ConcludingService : IConcludingService
    {
        private readonly IEngineGenerator _generator;
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<ConcludingService> _logger;

        public ConcludingService(IEngineGenerator generator, IQuestionRepository questionRepository, ILogger<ConcludingService> logger)
        {
            _generator = generator;
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<ConcludeResponse> Conclude(ConcludeRequest request)
        {

            var engine = await _generator.CreateEngine(request.Type);
            Clause currentClause = null;
            List<string> answersForCurrentQuestions = null;
            var unprovedConditions = new List<Clause>();

            if (request.Facts != null)
            {
                foreach (var fact in request.Facts.Select(clause => new IsClause(clause.Variable, clause.Value)))
                {
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
                    return new ConcludeResponse()
                    {
                        Conclusion = "Nie można było znaleźć odpowiedzi, skontaktuj się z serwisem"
                    };
                }
            }

            if (conclusion != null) return new ConcludeResponse()
            {
                Conclusion = $"{conclusion.Value}"
            };

            return new ConcludeResponse()
            {
                Question = currentClause.Variable,
                Answers = answersForCurrentQuestions,
                Facts = engine.Accessor.GetFacts(engine.Facts).Select(fact=> new IncomingClause()
                {
                    Condition = fact.Condition,
                    Value = fact.Value,
                    Variable = fact.Variable
                }).ToList()
            };
        }
    }
}
