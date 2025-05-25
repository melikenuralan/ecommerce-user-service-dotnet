using Microsoft.OpenApi.Models;
using Serilog;
using UserService.API.Extensions;
using UserService.Infrastructure;
using UserService.Persistence;
using UserService.Application;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddPersistenceService(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService", Version = "v1" });

    // JWT konfigürasyonu
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
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5500", "https://localhost:7277")
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



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseCors("LocalOrigins");
app.UseStaticFiles();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
}
else
{
    app.UseExceptionHandler("/error");
}
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
