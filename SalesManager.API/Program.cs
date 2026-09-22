using Microsoft.EntityFrameworkCore;
using SalesManager.Application.Interfaces;
using SalesManager.Application.Services;
using SalesManager.Infrastructure.Data;
using SalesManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Registrar repositorios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

// 3. Registrar servicios de aplicación
builder.Services.AddScoped<IClienteService, ClienteService>();

// 4. Controllers y OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();