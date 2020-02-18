using System.Collections.Generic;

namespace ExpertalSystem.Requests
{
    public class UpdateProblemRequest
    {
        public string ProblemName { get; set; }
        public List<QuestionRequest> Questions { get; set; }
        public string Solution { get; set; }
    }
}
