using Module.Application.Interfaces;
using Module.Application.Services;
using Module.Infrastructure.Extensions;
using Module.Infrastructure.Repositories;
using Module.Shared.Interfaces;
using Module.Shared.Middleware;
using Module.Shared.Logs;
using AspNetCore.Serilog.RequestLoggingMiddleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("BistroPulseDB");
builder.Services.AddCustomIdentity(connectionString);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISessionStoreRepository, SessionStoreRepository>();
builder.Services.AddScoped<ISessionStore, SessionStoreService>();
builder.Services.AddScoped<ISessionValidator, SessionStoreService>();
builder.Services.AddHostedService<SessionExpirationService>();

builder.Services.AddCustomLogging(builder.Configuration);
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

Console.WriteLine(app.Environment.EnvironmentName);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<AuthMiddleware>(); 
app.UseAuthorization();
app.MapControllers();
app.Run();
