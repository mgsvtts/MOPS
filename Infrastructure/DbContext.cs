using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;
public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    
    internal DbSet<MerchItem> MerchItems { get; set; }
    internal DbSet<Order> Orders { get; set; }
    internal DbSet<Models.Type> Types { get; set; }
}
