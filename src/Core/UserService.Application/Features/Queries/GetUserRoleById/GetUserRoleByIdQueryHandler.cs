using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs.Identity;

namespace UserService.Application.Features.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQueryHandler : IRequestHandler<GetUserRoleByIdQueryRequest, GetUserRoleByIdQueryResponse>
    {
        readonly IAuthService _authService;

        public GetUserRoleByIdQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GetUserRoleByIdQueryResponse> Handle(GetUserRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            // 1) Servisten dto'yu al
            UserRoleDto dto = await _authService.GetUserRoleByIdsAsync(request.UserId);

            // 2) Yeni response nesnesi oluşturup elle ata
            GetUserRoleByIdQueryResponse response = new GetUserRoleByIdQueryResponse
            {
                UserId = dto.UserId,
                UserName = dto.UserName,
                Roles = dto.Roles,
                Claims = dto.Claims
                             .Select(c => new Claim(c.Type, c.Value))
                             .ToList()
            };

            return response;
        }
    }
}
