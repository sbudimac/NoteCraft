using Microsoft.AspNetCore.Mvc;
using NoteCraftAPI.Service;
using NoteCraftModels;
using System.Security.Cryptography;

namespace NoteCraftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(string id)
        {
            var user = userService.GetById(id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found.");
            }

            return user;
        }

        [HttpPost("register")]
        public ActionResult<User> Register([FromBody] UserDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                var u = userService.GetByUsername(user.Username);
                if (u != null)
                {
                    ModelState.AddModelError("username", "Username is already in use.");
                    return BadRequest(ModelState);
                }
                return Ok(userService.Register(user));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while trying to register new user.");
            }
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserAuthDto request)
        {
            var user = userService.GetByUsername(request.Username);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = userService.CreateToken(user);

            return Ok(token);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
