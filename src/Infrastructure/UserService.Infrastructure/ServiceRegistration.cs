using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Abstractions.IServices;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenProvider, TokenProvider>();
        }
    }
}
