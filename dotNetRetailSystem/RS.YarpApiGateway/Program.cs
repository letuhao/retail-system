using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Retrieve the RateLimiter settings from the configuration
var rateLimiterConfig = builder.Configuration.GetSection("RateLimiter");

// Bind configuration values to local variables
string policyName = rateLimiterConfig.GetValue<string>("PolicyName");
int windowSeconds = rateLimiterConfig.GetValue<int>("windowSeconds");
int permitLimit = rateLimiterConfig.GetValue<int>("PermitLimit");

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter(policyName, options =>
    {
        options.Window = TimeSpan.FromSeconds(windowSeconds);
        options.PermitLimit = permitLimit;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
