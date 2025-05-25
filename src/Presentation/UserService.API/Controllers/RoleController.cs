using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;
using UserService.Application.Features.Commands.Roles.AssignRoleToUser;
using UserService.Application.Features.Queries.GetUserRoleById;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //  [Authorize(AuthenticationSchemes = "Admin")]
    public class RoleController : ControllerBase
    {

        private readonly IRoleService _roleService;
        private readonly ILogService _logger;
        private readonly IMediator _mediator;

        public RoleController(IRoleService roleService, ILogService logger, IMediator mediator)
        {
            _roleService = roleService;
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet("{userId:guid}/roles")]
        public async Task<IActionResult> GetUserRolesByIdAsync(
         [FromRoute] GetUserRoleByIdQueryRequest request)
        {
            GetUserRoleByIdQueryResponse response =
                await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("{userId:guid}/roles")]
        public async Task<IActionResult> AssignRoleToUser(
       [FromRoute] AssignRoleCommandRequest request)
        {
            AssignRoleCommandResponse response =
                await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(typeof(RoleDto), 201)]
        public async Task<ActionResult<RoleDto>> Create([FromBody] CreateRoleDto dto)
        {
            RoleDto created = await _roleService.CreateRoleAsync(dto.Name);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<RoleDto>> UpdateAsync(Guid id, [FromBody] UpdateRoleDto request)
        {
            _logger.Info($"[UpdateRole] Başlatılıyor: {id}");

            RoleDto updated = await _roleService.UpdateRoleAsync(id, request.Name);

            _logger.Info($"[UpdateRole] Başarılı: {updated.Id}");

            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.Info($"[DeleteRole] Başlatılıyor: {id}");

            await _roleService.DeleteRoleAsync(id);

            _logger.Info($"[DeleteRole] Başarılı: {id}");

            return NoContent();
        }
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
