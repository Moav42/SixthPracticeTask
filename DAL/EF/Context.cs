using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DAL.Entities;
using System.Configuration;

namespace DAL.EF
{
    public class Context : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public Context()
        {

        }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string connectionString = @"Data Source = LAPTOP-BPN6QIHG\SQLEXPRESS666; database = SixthPractice; integrated security = True; MultipleActiveResultSets = True; App = EntityFramework;";
            if (!optionsBuilder.IsConfigured)
            {              
                optionsBuilder.UseSqlServer(
                    connectionString, 
                    options => options.EnableRetryOnFailure());
            }
        }
      
    }
}
