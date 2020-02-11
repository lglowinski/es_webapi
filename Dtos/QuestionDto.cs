using System.Collections.Generic;

namespace ExpertalSystem.Dtos
{
    public class QuestionDto : Response
    {
        public List<string> Answeres { get; set; }
        public decimal QuestionPriority { get; set; }

        public QuestionTypes QuestionType { get; set; }
        public QuestionDto(string text) : base(text, ResponseType.Question)
        {

        }
    }

    public enum QuestionTypes
    {
        Screen,
        SlowPC,
        BrokenPC
    }
}
