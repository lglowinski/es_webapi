using System.Threading.Tasks;
using ExpertalSystem.Domain;

namespace ExpertalSystem.LogicServices
{
    public interface IEngineGenerator
    {
        public Task<RuleInferenceEngineExtension> CreateEngine();
        public Task<RuleInferenceEngineExtension> CreateEngine(IssueType issueType);
    }
}
