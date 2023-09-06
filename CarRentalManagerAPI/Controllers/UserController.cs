using CarRentalManagerAPI.Models.User;
using CarRentalManagerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarRentalManagerAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> Get([FromRoute] int id)
        {
            var user = _userService.GetById(id);

            return Ok(user);
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] CreateUserDto createUserDto)
        {
            var userId = _userService.RegisterUser(createUserDto);

            return Created($"api/users/{userId}", null);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            string token = _userService.GenerateJwt(loginUserDto);
            return Ok(token);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _userService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
        {
            _userService.Update(id, updateUserDto);

            return Ok();
        }
    }
}
