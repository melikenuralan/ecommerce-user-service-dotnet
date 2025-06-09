using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.ValueObjects;

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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            var user = new User(
                Guid.NewGuid(),
                new Email(dto.Email),
                new FullName(dto.FirstName, dto.LastName)
            );

            await _userRepository.AddAsync(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new UserDTO
            {
                Id = user.Id,
                Email = user.Email.Value,
                FullName = user.FullName.ToString()
            });
        }
    }
}
