using System;
using Microsoft.EntityFrameworkCore.Metadata;
#nullable disable

namespace WebDev.DataAccess.Entities
{
    using Microsoft.EntityFrameworkCore;
    using WebDev.Models;
    public partial class UsersDBContext : DbContext
    {
        public UsersDBContext()
        {
        }

        public UsersDBContext(DbContextOptions<UsersDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

    }
}