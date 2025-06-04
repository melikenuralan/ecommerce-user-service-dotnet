using MediatR;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs.Role;

namespace UserService.Application.Features.Commands.Roles.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandResponse>
    {
         readonly IRoleService _roleService;
         readonly ILogService _logger;

        public CreateRoleCommandHandler(IRoleService roleService, ILogService logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.Info($"[CreateRole] Başlatılıyor: {request.Name}");

            RoleDto createdRole = await _roleService.CreateRoleAsync(request.Name);

            _logger.Info($"[CreateRole] Başarılı: {createdRole.Name} ({createdRole.Id})");

            return new CreateRoleCommandResponse
            {
                Succeeded = true,
                ErrorMessage = null,
                RoleId = createdRole.Id
            };
        }
    }
}
