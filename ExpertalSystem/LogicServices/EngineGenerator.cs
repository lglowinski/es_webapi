using System.Threading.Tasks;
using chen0040.ExpertSystem;
using ExpertalSystem.Domain;
using ExpertalSystem.Repositories;

namespace ExpertalSystem.LogicServices
{
    public class EngineGenerator : IEngineGenerator
    {
        private readonly IProblemRepository _problemRepository;
        public EngineGenerator(IProblemRepository problemRepository)
        {
            _problemRepository = problemRepository;
        }
        public async Task<RuleInferenceEngineExtension> CreateEngine()
        {
            var engine = new RuleInferenceEngineExtension();
            var problems = await _problemRepository.FindAsync();
            if (problems is null) return engine;

            foreach(var problem in problems)
            {
                var rule = new Rule(problem.ProblemName);
                foreach(var question in problem.Questions)
                {
                    rule.AddAntecedent(new IsClause(question.QuestionName, question.Answer));
                }
                rule.setConsequent(new IsClause(nameof(problem.Solution), problem.Solution));
                engine.AddRule(rule);
            }

            return engine;
        }
        public async Task<RuleInferenceEngineExtension> CreateEngine(IssueType issueType)
        {
            var engine = new RuleInferenceEngineExtension();
            var problems = await _problemRepository.FindAsync(issueType);
            if (problems is null) return engine;

            foreach (var problem in problems)
            {
                var rule = new Rule(problem.ProblemName);
                foreach (var question in problem.Questions)
                {
                    rule.AddAntecedent(new IsClause(question.QuestionName, question.Answer));
                }
                rule.setConsequent(new IsClause(nameof(problem.Solution), problem.Solution));
                engine.AddRule(rule);
            }

            return engine;
        }
    }
}
