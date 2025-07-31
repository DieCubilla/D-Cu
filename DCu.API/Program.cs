using DCu.Domain.Interfaces.Security;
using DCu.Infrastructure.Persistences;
using DCu.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddHttpClient<IGeocodingService, NominatimGeocodingService>(client =>
{
    client.DefaultRequestHeaders.Add("User-Agent", "D-Cu API - Contacto: cubillita@gmail.com");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
