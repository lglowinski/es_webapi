using System.Collections.Generic;

namespace ExpertalSystem.Domain
{
    public class Question : BaseEntity
    {
     public string QuestionName { get; set; }
     public List<string> Answers { get; set; } = new List<string>();
    }
}
