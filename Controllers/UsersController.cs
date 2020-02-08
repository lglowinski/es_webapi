using ExpertalSystem.Authorization;
using ExpertalSystem.Dtos;
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
        public async Task<IActionResult> Auth([FromBody] AuthenticateRequest authenticateRequest)
        {
            var user = await _userRepository.GetAsync(p => p.name.Equals(authenticateRequest.Login) && p.Password.Equals(authenticateRequest.Password));
            if (user != null)
            {
                var token = JWTManager.GenerateToken(authenticateRequest.Login);
                return Ok(new JwtToken { Token = token });
            }
            return BadRequest("User not found");
        }
    }
}
