using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Features.Handler
{
    public interface IQueryHandler<TQuery,TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
