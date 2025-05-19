using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyBank.Domain.Entities;

namespace MoneyBank.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Opcional: puedes configurar validaciones, relaciones, etc. aquí.
    }
}
