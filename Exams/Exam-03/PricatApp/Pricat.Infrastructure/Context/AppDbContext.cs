using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DbSet<Categories> Categories { get; set; }

        public DbSet<Products> Products { get; set; }

        internal object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
