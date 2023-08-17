using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto_entityframework;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ProyectoContext>(p => p.UseInMemoryDatabase("MiBaseEnMemoria"));
builder.Services.AddSqlServer<ProyectoContext>(builder.Configuration.GetConnectionString("conexionBd"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] ProyectoContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());

});

app.Run();
