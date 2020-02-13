using ExpertalSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Requests
{
    public class CreateProblemRequest
    {
        public string ProblemName { get; set; }
        public List<QuestionRequest> Questions { get; set; }
        public IssueType IssueType { get; set; }
        public string Solution { get; set; }
    }

    public class QuestionRequest
    {
        public string QuestionName { get; set; }
        public string Answer { get; set; }
    }
}
