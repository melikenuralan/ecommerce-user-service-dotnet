using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Application
{
    public static class Registration
    {

        public static void AddApplication(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
 
        }
    }
}
