using ExpertalSystem.LogicServices;
using ExpertalSystem.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Controllers
{
    public class ConcludingControler : BaseController
    {
        private readonly IQuestionService _questionService;
        public ConcludingControler(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpPost]
        public async Task<ActionResult<object>> Conclude(ConcludeRequest request)
        {
            var result = await _questionService.Conclude(request);
            return Ok(result);
        }
    }
}
