using System.Threading.Tasks;
using ExpertalSystem.LogicServices;
using ExpertalSystem.Requests;
using Microsoft.AspNetCore.Mvc;

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
