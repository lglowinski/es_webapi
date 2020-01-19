using ExpertalSystem.Dtos;
using ExpertalSystem.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.LogicServices
{
    public class LogicService : ILogicService
    {
        private readonly IQuestionService _questionService;
        public LogicService(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<CommonResult<Question>> CalculateQuestion(Question questionBefore = default, string answare = default)
        {
            if (questionBefore == default && answare == default)
            {
                var firstQuestion = _questionService.GetFirstQuestion();
                return new CommonResult<Question>(firstQuestion);
            }
            var nextQuestion = await _questionService.GetNextQuestion(questionBefore, string answare);
            if(nextQuestion)
        }
    }
}
