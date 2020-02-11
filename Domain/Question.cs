using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Domain
{
    public class Question : BaseEntity
    {
        public string RuleName { get; set; }
        public List<ClausesBasic> Clauses { get; set; } = new List<ClausesBasic>();
        public IssueTypes IssueTypes { get; set; }
        public string Consequence { get; set; }
    }
    public class ClausesBasic
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
    }
}
