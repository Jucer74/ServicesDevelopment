﻿using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Models;

namespace MoneyBankService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Account{ get; set; }
}