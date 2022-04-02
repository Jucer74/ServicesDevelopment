using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ReminderApp.Domain;
using ReminderApp.Domain.Entities;

namespace ReminderApp.Infrastructure.Context
{
    public partial class AppDbcontext : DbContext
    {
        public AppDbcontext()
        {
        }

        public AppDbcontext(DbContextOptions<AppDbcontext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Reminder> Users { get; set; }
    }
}