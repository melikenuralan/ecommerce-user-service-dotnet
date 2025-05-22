using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Abstractions.IServices;

namespace UserService.Application.Features.Commands.Roles.CreateRole
{
    public class CreateRoleCommandHandler
    {
        //todo : fix bugs
        private readonly IRoleService _roleService;
        private readonly ILogService _logger;

        public CreateRoleCommandHandler(IRoleService roleService, ILogService logger)
        {
            _roleService = roleService;
            _logger = logger;
        }
        public async Task<CreateRoleCommandResponse> HandleAsync(
          CreateRoleCommandRequest request,
          CancellationToken cancellationToken = default)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            _logger.Info($"[CreateRole] Başlatılıyor: {request.Name}");


            // IRoleService.CreateRoleAsync'unü dönüş tipi RoleId ise ona göre ayarla
            bool created = await _roleService.CreateRoleAsync(request.Name);


            if (created)
            {
                _logger.Info($"[CreateRole] Başarılı: {request.Name}");


                return new CreateRoleCommandResponse
                {
                    Succeeded = true,
                    ErrorMessage = null
                };
            }
            else
            {
                _logger.Error($"[CreateRole] Hata: {request.Name}");
                return new CreateRoleCommandResponse
                {
                    Succeeded = false,
                    ErrorMessage = null
                };
            }
        }
    }
}
