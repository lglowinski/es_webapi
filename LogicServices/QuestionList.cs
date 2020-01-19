using ExpertalSystem.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.LogicServices
{
    public class QuestionList
    {
        public static List<Question> ScreenProblems = new List<Question>
        {
            new Question("Is electricity pluged into monitor")
            {
                Answeres = new List<string>
                {
                    "Yes",
                    "No"
                },
                QuestionPriority = 1,
                QuestionType = QuestionTypes.Screen
            },
            new Question("Is HDMI/VGA/DVI/Display-port pluged into monitor")
            {
                Answeres = new List<string>
                {
                    "Yes",
                    "No"
                },
                QuestionPriority = 2,
                QuestionType = QuestionTypes.Screen
            },
            new Question("Is any other monitor working?")
            {
                Answeres = new List<string>
                {
                    "Yes",
                    "No"
                },
                QuestionPriority = 3,
                QuestionType = QuestionTypes.Screen
            }
        };

        public static List<Question> SlowPCProblems = new List<Question>
        {
            new Question("")
        };

        public static List<Question> BrokenPCProblems = new List<Question>
        {
            new Question("")
        };

        public static List<Question> IOProblems = new List<Question>
        {
            new Question("")
        };
    }

}
