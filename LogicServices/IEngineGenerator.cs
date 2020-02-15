using chen0040.ExpertSystem;
using ExpertalSystem.Domain;
using System.Threading.Tasks;

namespace ExpertalSystem.LogicServices
{
    public interface IEngineGenerator
    {
        public Task<RuleInferenceEngineExtension> CreateEngine();
        public Task<RuleInferenceEngineExtension> CreateEngine(IssueType issueType);
    }
}
