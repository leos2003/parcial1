using Microsoft.EntityFrameworkCore;
using parcial1.Domain;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Servicios
builder.Services.AddControllers();

builder.Services.AddDbContext<SupermercadoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Build
var app = builder.Build();

// 🔹 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
