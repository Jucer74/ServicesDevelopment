using PizzeriaApi.Models;
using Microsoft.EntityFrameworkCore;


namespace PizzeriaApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pizzeria> Pizzas { get; set; }
    public DbSet<PizzeriaCategoria> PizzasCategoria { get; set; }
}
