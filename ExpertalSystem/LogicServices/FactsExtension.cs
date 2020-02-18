using System.Collections.Generic;
using System.Reflection;
using chen0040.ExpertSystem;

namespace ExpertalSystem.LogicServices
{
    public class FactsExtension : WorkingMemory
    {
        public List<Clause> GetFacts(WorkingMemory wm) => wm.GetType().GetField("_facts", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(wm) as List<Clause>;
    }
}
