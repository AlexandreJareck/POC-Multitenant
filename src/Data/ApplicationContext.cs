using Microsoft.EntityFrameworkCore;
using src.Domain;

namespace src.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Product> Products { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }
}
