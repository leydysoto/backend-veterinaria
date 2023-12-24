using Microsoft.EntityFrameworkCore;
using veterinaria.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//aqui configuración*****
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
builder.Services.AddDbContext<veterinariaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("veterinariaContext"));
});
//aqui cors****

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//aquii activar politica  cors
app.UseCors("NuevaPolitica");

app.MapControllers();

app.Run();
