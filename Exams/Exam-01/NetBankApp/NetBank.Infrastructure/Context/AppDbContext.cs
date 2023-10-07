<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
﻿using Microsoft.EntityFrameworkCore;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
using NetBank.Domain.Models;

namespace NetBank.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<IssuingNetwork> IssuingNetworks { get; set; }
}