using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Interfaces;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);

            if (user is null) return NotFound();

            return Ok(user);
        }
    }
}
