using System.Collections.Generic;
using ExpertalSystem.Domain;

namespace ExpertalSystem.Requests
{
    public class CreateProblemRequest
    {
        public string ProblemName { get; set; }
        public List<QuestionRequest> Questions { get; set; } = new List<QuestionRequest>();
        public IssueType IssueType { get; set; }
        public string Solution { get; set; }
    }

    public class QuestionRequest
    {
        public string QuestionName { get; set; }
        public string Answer { get; set; }
    }
}
