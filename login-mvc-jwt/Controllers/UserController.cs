using login_mvc_jwt.Dto.Users;
using login_mvc_jwt.repository.Users;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace login_mvc_jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IRepositoryUsers _repo;

        public UserController(IRepositoryUsers repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register (registerDto registerDto)
        {
            var result = await _repo.register(registerDto);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(loginDto loginDto)
        {
            try
            {
                var user = await _repo.login(loginDto);

                if (user == null) return BadRequest("");

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
