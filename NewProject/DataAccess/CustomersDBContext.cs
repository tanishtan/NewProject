using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppADO.DataAccess
{
    public class CustomersDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        // property name shoulf match table name and should be plulars
        // this property name will be used as table name while executing
        public DbSet<Customer> Customers { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                connectionString : @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;
           multipleactiveresultsets=true"
                );

            // Create a logger to inspect the SQL
            optionsBuilder.LogTo(Console.Out.WriteLine);
        }
    }
}
