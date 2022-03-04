using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev.Api.Context
{
    using Microsoft.EntityFrameworkCore;
    using WebDev.Api.Model;

    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}