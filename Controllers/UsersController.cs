using System;
using System.Threading.Tasks;
using ExpertalSystem.Authorization;
using ExpertalSystem.Domain;
using ExpertalSystem.Dtos;
using ExpertalSystem.Repositories;
using ExpertalSystem.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ExpertalSystem.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtManager _jWTManager;

        public UsersController(IUserRepository userRepository, IJwtManager jWTManager)
        {
            _userRepository = userRepository;
            _jWTManager = jWTManager;
        }

        /// <summary>
        /// Creates new user in database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if ((await _userRepository.GetAsync(p=>p.Name.Equals(request.Name)) != null))
                return BadRequest("User with this name already exists");

            var user = new User()
            {
                Name = request.Name,
                Password = Hasher.HashPassword(request.Password),
                Id = Guid.NewGuid()
            };
            await _userRepository.AddAsync(user);
            return Created("userName", user);
        }

        /// <summary>
        /// Authorize to get JWT token
        /// </summary>
        /// <param name="authenticateRequest"></param>
        /// <returns></returns>
        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody] AuthenticateRequest authenticateRequest)
        {
            var fetchedUser = await _userRepository.GetAsync(x=>x.Name.Equals(authenticateRequest.Login));
            if (fetchedUser is null) return NotFound("User with this name was not found");

            if (Hasher.Encrypt(authenticateRequest.Password, fetchedUser.Password))
            {
                var token = _jWTManager.GenerateToken(fetchedUser.Id.ToString(), fetchedUser.Name, 43200);
                return Ok(new JwtToken { Token = token });
            }
            return BadRequest("Wrong password");
        }
    }
}
