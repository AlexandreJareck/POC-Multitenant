using Estrategia03.Data;
using Estrategia03.Data.Interceptors;
using Estrategia03.Middlewares;
using Estrategia02.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estrategia03.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TenantData>();
builder.Services.AddScoped<StrategySchemaInterceptor>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ApplicationContext>(provider =>
{
    var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
    var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
    var tenantId = httpContext?.GetTenantId();
    var connectionString = builder.Configuration.GetConnectionString(tenantId!);

    optionsBuilder
        .UseSqlServer(connectionString)
        .LogTo(Console.WriteLine)
        .EnableSensitiveDataLogging();

    return new ApplicationContext(optionsBuilder.Options);
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