using chen0040.ExpertSystem;

namespace ExpertalSystem.LogicServices
{
    public class RuleInferenceEngineExtension : RuleInferenceEngine
    {
        public WorkingMemory GetClauses() => base.Facts;
        public FactsExtension Accessor = new FactsExtension();
    }
}
