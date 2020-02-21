using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExpertalSystem.Domain;

namespace ExpertalSystem.Requests
{
    public class CreateProblemRequest
    {
        [Required]
        public string ProblemName { get; set; }
        [Required]
        public List<QuestionRequest> Questions { get; set; } = new List<QuestionRequest>();
        [Required]
        public IssueType IssueType { get; set; }
        [Required]
        public string Solution { get; set; }
    }

    public class QuestionRequest
    {
        [Required]
        public string QuestionName { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
