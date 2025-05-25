using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace UserService.Application.Features.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQueryRequest : IRequest<GetUserRoleByIdQueryResponse>
    {
        public Guid UserId { get; set; }

    }
}
