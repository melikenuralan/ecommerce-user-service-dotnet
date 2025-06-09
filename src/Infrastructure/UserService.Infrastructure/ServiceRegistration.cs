using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {



            //services.AddMassTransit(x =>
            //{
            //    x.UsingRabbitMq((context, cfg) =>
            //    {
            //        cfg.Host("localhost", "/", h =>
            //        {
            //            h.Username("guest");
            //            h.Password("guest");
            //        });
            //    });
            //});

        }
    }
}
