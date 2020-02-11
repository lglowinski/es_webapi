using ExpertalSystem.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Requests
{
    public class GetQuestionRequest
    {
        public QuestionType QuestionType { get; set; }

        #nullable enable
        public string? SessionId { get; set; }
    }

    public enum QuestionType
    {
        ScreenQuestion,
        HardwareQuestion,
        IOQuestion
    }
}
