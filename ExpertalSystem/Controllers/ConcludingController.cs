using System.Threading.Tasks;
using ExpertalSystem.Dtos;
using ExpertalSystem.LogicServices;
using ExpertalSystem.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ExpertalSystem.Controllers
{
    [Route("concluding")]
    public class ConcludingController : BaseController
    {
        private readonly IConcludingService _concludingService;
        public ConcludingController(IConcludingService concludingService)
        {
            _concludingService = concludingService;
        }

        [HttpPost]
        public async Task<ActionResult<ConcludeResponse>> Conclude([FromBody]ConcludeRequest request)
        {
            var result = await _concludingService.Conclude(request);
            return Ok(result);
        }
    }
}
