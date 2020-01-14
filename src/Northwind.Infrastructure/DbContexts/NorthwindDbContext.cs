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
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<OrderDetail>()
            //            .HasOne(o => o.Order)
            //            .WithMany(t => t.OrderDetails)
            //            .IsRequired()
            //            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                        .HasKey(o => new { o.OrderID, o.ProductID });
        }
    }
}
