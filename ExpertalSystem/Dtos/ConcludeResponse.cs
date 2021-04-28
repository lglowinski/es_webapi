#nullable enable
using System.Collections.Generic;
using ExpertalSystem.Requests;

namespace ExpertalSystem.Dtos
{
    public class ConcludeResponse
    {
        public string? Question { get; set; }
        public List<string>? Answers { get; set; }
        public List<IncomingClause>? Facts { get; set; }
        public string? Conclusion { get; set; }
    }
}
