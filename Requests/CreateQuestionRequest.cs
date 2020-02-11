using ExpertalSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Requests
{
    public class CreateQuestionRequest
    {
        public string RuleName { get; set; }
        public List<QuestionRequest> ClausesFields { get; set; }
        public IssueTypes IssueType { get; set; }
        public string Consequence { get; set; }
    }

    public class QuestionRequest
    {
        public string Clause { get; set; }
        public string Answer { get; set; }
    }
}
