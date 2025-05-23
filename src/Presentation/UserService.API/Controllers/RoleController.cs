using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

  //  [Authorize(AuthenticationSchemes = "Admin")]
    public class RoleController : ControllerBase
    {

        private readonly IRoleService _roleService;
        private readonly ILogService _logger;

        public RoleController(IRoleService roleService, ILogService logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        // POST: api/Roles
        [HttpPost]
        [ProducesResponseType(typeof(RoleDto), 201)]
        public async Task<ActionResult<RoleDto>> Create([FromBody] CreateRoleDto dto)
        {
            RoleDto created = await _roleService.CreateRoleAsync(dto.Name);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        // PUT: api/Roles/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<RoleDto>> UpdateAsync(Guid id, [FromBody] UpdateRoleDto request)
        {
            _logger.Info($"[UpdateRole] Başlatılıyor: {id}");

            RoleDto updated = await _roleService.UpdateRoleAsync(id, request.Name);

            _logger.Info($"[UpdateRole] Başarılı: {updated.Id}");

            return Ok(updated);
        }

        // DELETE: api/Roles/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.Info($"[DeleteRole] Başlatılıyor: {id}");

            await _roleService.DeleteRoleAsync(id);

            _logger.Info($"[DeleteRole] Başarılı: {id}");

            return NoContent();
        }
        // GET: api/Roles
        [HttpGet]
        public ActionResult<IList<RoleDto>> GetAllRoles()
        {
            IDictionary<Guid, string> rolesDict = _roleService.GetAllRoles();
            IList<RoleDto> roles = rolesDict
                .Select(kvp => new RoleDto
                {
                    Id = kvp.Key,
                    Name = kvp.Value,
                    Permissions = new List<string>()
                })
                .ToList();

            return Ok(roles);
        }

        // GET: api/Roles/{id}
        [HttpGet("{id:guid}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RoleDto>> GetByIdAsync(Guid id)
        {
            (Guid roleId, string roleName) = await _roleService.GetRoleByIdAsync(id);
            if (string.IsNullOrEmpty(roleName))
                return NotFound();

            var dto = new RoleDto
            {
                Id = roleId,
                Name = roleName,
                Permissions = new List<string>()
            };
            return Ok(dto);
        }
    }
}
