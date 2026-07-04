using Microsoft.EntityFrameworkCore;
using OnboardingApp.Api.Data;
using OnboardingApp.Api.Repositories;
using OnboardingApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Register OnboardingDbContext with Npgsql (PostgreSQL)
builder.Services.AddDbContext<OnboardingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository and Service abstractions
builder.Services.AddScoped<IOnboardingRepository, OnboardingRepository>();
builder.Services.AddScoped<IOnboardingService, OnboardingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map controller routes
app.MapControllers();

app.Run();
