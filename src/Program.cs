using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Domain;
using src.Middlewares;
using src.Provider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TenantData>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(p => p
.UseSqlServer("Server=JARECK\\SQLEXPRESS;Database=Multitenant01;Integrated Security=true;pooling=true;TrustServerCertificate=True")
.LogTo(Console.WriteLine)
.EnableSensitiveDataLogging());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<TenantMiddleware>();

app.MapGet("/person", ([FromServices] ApplicationContext db) =>
{
    var person = db.People.ToArray();

    return person;
})
.WithName("GetPerson")
.WithOpenApi();

app.MapGet("/product", ([FromServices] ApplicationContext db) =>
{
    var product = db.Products.ToArray();

    return product;
})
.WithName("GetProduct")
.WithOpenApi();

app.Run();


