using UserService.Persistence;
using Serilog;
using UserService.Infrastructure;
using UserService.Application.Features.Commands.UserAuth.LoginUser;
using UserService.Application.Features.Commands.UserAuth.RegisterUser;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddPersistenceService(builder.Configuration);


builder.Services.AddScoped<LoginUserCommandHandler>();
builder.Services.AddScoped<RegisterUserCommandHandler>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService", Version = "v1" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            securitySchema,
            new[] { "Bearer" }
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});


//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(5000); // HTTP
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSwagger",
        policy =>
        {
            policy
              .WithOrigins("http://localhost:5000", "https://localhost:5001")
              .AllowAnyHeader()
              .AllowAnyMethod();
        });
});

///serilog configuration --------
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
///------------------------------



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



 if (app.Environment.IsDevelopment())
 {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
