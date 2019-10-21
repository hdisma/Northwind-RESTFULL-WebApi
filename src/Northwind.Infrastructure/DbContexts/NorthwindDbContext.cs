using Microsoft.EntityFrameworkCore;
using Northwind.Core.Entities.Northwind;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Infrastructure.DbContexts
{
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
