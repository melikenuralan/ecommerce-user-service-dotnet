using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = "Admin")]
    public class RoleController : ControllerBase
    {

        public RoleController()
        {
            
        }
        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> GetAllRoles([FromQuery] GetRolesQueryRequest request)
        //{
        //    var response = await _mediator.Send(request);
        //    return Ok(response);
        //}
        //[HttpGet("{Id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> GetRoleById([FromRoute] GetRoleByIdQueryRequest request)
        //{
        //    var response = await _mediator.Send(request);
        //    return Ok(response);
        //}
        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest request)
        //{
        //    var response = await _mediator.Send(request);
        //    return Ok(response);
        //}
        //[HttpPut("{Id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> UpdateRole([FromBody, FromRoute] UpdateRoleCommandRequest request)
        //{
        //    var response = await _mediator.Send(request);
        //    return Ok(response);
        //}
        //[HttpDelete("{Id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest request)
        //{
        //    var response = await _mediator.Send(request);
        //    return Ok(response);
        //}
    }
}
