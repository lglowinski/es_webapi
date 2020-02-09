using ExpertalSystem.Authorization;
using ExpertalSystem.Domain;
using ExpertalSystem.Dtos;
using ExpertalSystem.Repositories;
using ExpertalSystem.Requests;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
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

        /// <summary>
        /// Creates new user in database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (await _userRepository.GetAsync(request.Name) != null) return BadRequest("User with this name already exists");

            var generator = new ObjectIdGenerator();
            var user = new User()
            {
                Name = request.Name,
                Password = Hasher.HashPassword(request.Password),
                Id = (ObjectId) (generator.GenerateId("users", $"{request.Name}{request.Password}"))
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
            var fetchedUser = await _userRepository.GetAsync(authenticateRequest.Login);
            if (fetchedUser is null) return NotFound("User with this name was not found");

            if (Hasher.Encrypt(authenticateRequest.Password, fetchedUser.Password))
            {
                var token = JWTManager.GenerateToken(fetchedUser.Id.ToString(), fetchedUser.Name);
                return Ok(new JwtToken { Token = token });
            }
            return BadRequest("Wrong password");
        }
    }
}
