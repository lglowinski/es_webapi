using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Requests
{
    public class CreateQuestionRequest
    {
        public string RuleName { get; set; }
        public List<Question> ClausesFields { get; set; }
        public string Consequence { get; set; }
    }

    public class Question
    {
        public string Clause { get; set; }
        public string Answer { get; set; }
    }
}
