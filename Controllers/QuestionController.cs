using ExpertalSystem.Dtos;
using ExpertalSystem.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Response>> GetQuestion([FromQuery] GetQuestionRequest request)
        {
            return Ok($"{request.PreviousQuestion} / {request.IsFirstQuestion}");
        }
    }
}
