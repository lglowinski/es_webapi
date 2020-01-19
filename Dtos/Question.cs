using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Dtos
{
    public class Question : Response
    {
        public List<string> Answeres { get; set; }
        public decimal QuestionPriority { get; set; }

        public QuestionTypes QuestionType { get; set; }
        public Question(string text) : base(text, ResponseType.Question)
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
