using chen0040.ExpertSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ExpertalSystem.LogicServices
{
    public class FactsExtension : WorkingMemory
    {
        public object GetFacts(WorkingMemory wm) => wm.GetType().GetField("_facts", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(wm);
    }
}
