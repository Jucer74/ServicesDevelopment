using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Entities;

namespace Pricat.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> categories { get; set; }

        public DbSet<Product> products { get; set; }
    }
}
