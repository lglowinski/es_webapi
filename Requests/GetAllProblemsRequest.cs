using ExpertalSystem.Domain;

namespace ExpertalSystem.Requests
{
    public class GetAllProblemsRequest
    {
        public IssueTypes IssueType { get; set; }
    }
}
