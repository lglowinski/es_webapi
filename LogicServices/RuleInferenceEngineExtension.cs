using chen0040.ExpertSystem;

namespace ExpertalSystem.LogicServices
{
    public class RuleInferenceEngineExtension : RuleInferenceEngine
    {
        public FactsExtension Accessor { get; set; } = new FactsExtension();
    }
}
