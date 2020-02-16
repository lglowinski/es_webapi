namespace ExpertalSystem.Requests
{
    public class GetProblemRequest
    {
        public ProblemType ProblemType { get; set; }

        #nullable enable
        public string? SessionId { get; set; }
    }

    public enum ProblemType
    {
        ScreenQuestion,
        HardwareQuestion,
        IOQuestion
    }
}
