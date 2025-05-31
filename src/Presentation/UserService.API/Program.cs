using Microsoft.OpenApi.Models;
using Serilog;
using UserService.API.Extensions;
using UserService.Infrastructure;
using UserService.Persistence;
using UserService.Application;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddPersistenceService(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService API", Version = "v1", Description = "UserService API swagger client." });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the token after typing 'Bearer' followed by a space. \r\n\r\n For Instance: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }

    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5174") // React dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
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
//geçici
app.UseDeveloperExceptionPage();
//

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
