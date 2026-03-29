using Module.Application.Interfaces;
using Module.Application.Services;
using Module.Infrastructure.Extensions;
using Module.Infrastructure.Repositories;
using Module.Shared.Interfaces;
using Module.Shared.Middleware;
using Module.Shared.Logs;
using Serilog;
using Asp.Versioning.Conventions;
using Module.Application.Interfaces.IRepositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.ReportApiVersions = true;
}).AddMvc(options =>
{
    options.Conventions.Add(new VersionByNamespaceConvention());
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(5020); 
//});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("BistroPulseDB");
builder.Services.AddCustomIdentity(connectionString);



builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISessionStore, SessionStoreService>();
builder.Services.AddHostedService<SessionExpirationService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

builder.Services.AddScoped<ISessionStoreRepository, SessionStoreRepository>();
builder.Services.AddScoped<ISessionValidator, SessionStoreService>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();


builder.Services.AddCustomLogging(builder.Configuration);
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options =>
                options.WithOrigins("http://localhost:4200", "https://bistro-pulse.vercel.app")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

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
