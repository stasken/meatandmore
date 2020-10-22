using MeatAndMoreAPI.Data;
using MeatAndMoreAPI.DTOs;
using MeatAndMoreAPI.Repositories;
using MeatAndMoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MeatAndMoreContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UsersController(MeatAndMoreContext context, IUserRepository userRepository, IUserService userService)
        {
            _context = context;
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsers().ConfigureAwait(false));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserDetails(string id)
        {
            var user = await _userRepository.GetUserDetails(id).ConfigureAwait(false);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserLoginDTO userLoginDTO)
        {
            var userResult = await _userService.Login(userLoginDTO.UserName, userLoginDTO.Password).ConfigureAwait(false);

            if (userResult == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return CreatedAtAction("GetUserDetails", new { id = userResult.Id }, userResult);
        }
    }
}
