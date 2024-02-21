using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Domain;

var builder = WebApplication.CreateBuilder(args);
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

DatabaseInitialize(app);
app.UseHttpsRedirection();


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

void DatabaseInitialize(IApplicationBuilder app)
{
    using var db = app.ApplicationServices
        .CreateScope()
        .ServiceProvider
        .GetRequiredService<ApplicationContext>();

    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    for (var i = 1; i <= 5; i++)
    {
        db.People.Add(new Person { Name = $"Person {i}" });
        db.Products.Add(new Product { Description = $"Product {i}" });
    }

    db.SaveChanges();
}
