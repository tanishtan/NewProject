using ConsoleAppADO.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ConsoleAppADO.Repositories
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName}, {LastName}"; }
        public DateTime HireDate { get; set; }
    }

    //Context class 
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Customer> customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                connectionString: @"server=(local);database=northwind;integrated security=sspi;trustservercertificate=true"
                );
        }

        public DbSet<Employee> Employees { get; set; }

    }
}


