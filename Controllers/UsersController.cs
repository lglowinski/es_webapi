using ExpertalSystem.Authorization;
using ExpertalSystem.Repositories;
using ExpertalSystem.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("auth")]
        public IActionResult Auth([FromBody] AuthenticateRequest authenticateRequest)
        {
            var token = JWTManager.GenerateToken(authenticateRequest.Login);
            //if (_userRepository.GetAsync(p=>p.Login == authenticateRequest.Login && p.Password == authenticateRequest.Password) != null)
            //{
            //    token = JWTManager.GenerateToken(authenticateRequest.Login);
            //    return Ok(token);
            //}
            return BadRequest(token);
        }
    }
}
