using API.Configurations;
using API.Policies;
using Application;
using Infrastructure;
using Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();

builder.Services.AddSwagger();

builder.Services.AddPersistenceServices()
    .AddInfrastructureServices()
    .AddApplicationServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddRateLimiter(options => {
    
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy<string, SingleRequestPerIpPolicy>("SingleRequestPerIP");
    options.AddPolicy<string, FiveRequestsPerIpPolicy>("FiveRequestsPerIP");
});

var app = builder.Build();

await DatabaseStartupConfigurator.ApplyMigrations(app);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler("/error");

app.UseRateLimiter();

app.MapControllers();

app.Run();