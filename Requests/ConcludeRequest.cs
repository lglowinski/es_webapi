using System.Collections.Generic;
using ExpertalSystem.Domain;

namespace ExpertalSystem.Requests
{
    public class ConcludeRequest
    {
        #nullable enable
        public string? Answer { get; set; }
        public string? PreviousQuestion { get; set; }
        public List<IncomingClause>? Facts { get; set; }
        #nullable disable
        public IssueType Type { get; set; }
    }


}
