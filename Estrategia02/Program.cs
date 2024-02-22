using Estrategia02.Data;
using Estrategia02.Data.Interceptors;
using Estrategia02.Middlewares;
using Estrategia02.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<TenantData>();
builder.Services.AddScoped<StrategySchemaInterceptor>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>((provider, options) =>
{
    options
        .UseSqlServer("Server=JARECK\\SQLEXPRESS;Database=Multitenant02;Integrated Security=true;pooling=true;TrustServerCertificate=True")
        .LogTo(Console.WriteLine)
        .EnableSensitiveDataLogging();

    var interceptor = provider.GetRequiredService<StrategySchemaInterceptor>();

    options.AddInterceptors(interceptor);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<TenantMiddleware>();

app.MapGet("{tenant}/person", ([FromServices] ApplicationContext db) =>
{
    var person = db.People.ToArray();

    return person;
})
.WithName("GetPerson")
.WithOpenApi();

app.MapGet("{tenant}/product", ([FromServices] ApplicationContext db) =>
{
    var product = db.Products.ToArray();

    return product;
})
.WithName("GetProduct")
.WithOpenApi();

app.Run();