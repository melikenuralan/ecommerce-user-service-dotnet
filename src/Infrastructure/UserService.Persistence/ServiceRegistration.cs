﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Interfaces;
using UserService.Persistence.Concretes.Repositories;
using UserService.Persistence.Data;


namespace UserService.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration
                                     .GetConnectionString("UserDbConnection")
                                     ?? throw new InvalidOperationException("ConnectionStrings:UserDbConnection tanımlı değil.");

            services.AddDbContext<UserServiceDbContext>(opts =>
                                      opts.UseNpgsql(connectionString));


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserSettingsRepository,UserSettingsRepository>();
            services.AddScoped<IBlockedUserRepository,BlockedUserRepository>();
        }
    }
}
