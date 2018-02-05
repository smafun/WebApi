using System;
using Microsoft.EntityFrameworkCore;
using ExtendedWebApi.Models;
namespace ExtendedWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

        /*       protected override void OnModelCreating(ModelBuilder modelbuilder)
               {
                    modelbuilder.Entity<Custom>().ToTable("Customer");
                    modelbuilder.Entity<Order>().ToTable("Order");
                    modelbuilder.Entity<ServiceType>().ToTable("ServiceTypes");
               }

        */
    }
}
