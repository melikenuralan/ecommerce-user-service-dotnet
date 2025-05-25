using System.Reflection;
using Microsoft.OpenApi.Models;
using Serilog;
using UserService.API.Extensions;
using UserService.Application.Features.Commands.UserAuth.LoginUser;
using UserService.Application.Features.Commands.UserAuth.RegisterUser;
using UserService.Infrastructure;
using UserService.Persistence;


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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(5000); // HTTP
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalOrigins", policy =>
    {
        policy
          .WithOrigins(
            "http://localhost:5500",     // Google-login.html’i çalýþtýrdýðýnýz origin
            "https://localhost:7277"     // Swagger UI (opsiyonel, ayný origin olduðu için genelde gerekmez)
          )
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
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("UserService.Application"));
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("LocalOrigins");
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();   // <-- Bu satýrý ekleyin veya aktif edin
}
else
{
    app.UseExceptionHandler("/error"); // Prod için halihazýrdaki handler
}
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
