using ExpertalSystem.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.LogicServices
{
    public class QuestionService : IQuestionService
    {
        private Question firstQuestion = new Question("What is not working?")
        {
            Answeres = new List<string>
            {
                "Screen is not working",
                "PC is not turning on",
                "PC is working slow",
                "Mouse/keyboard are not working"
            }
        };

        public Question GetFirstQuestion() => firstQuestion;

        public Question GetNextQuestion(Question previouseQuestion, string answare)
        {
            if (IsResponseFound(previouseQuestion))
                return default;
            return new Question("");
        }

        private bool IsResponseFound(Question previouseQuestion)
        {
            //if (previouseQuestion)

                return false;
        }
    }
}
