using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Entities;

namespace Pricat.Infrastructure.Contex
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
