﻿using loremipsum.Entities;
using Microsoft.EntityFrameworkCore;

namespace loremipsum.Persistence
{
    public class EmployeeContext : DbContext
    {
        public virtual DbSet<Employee> Members { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = Employees;Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
                modelBuilder.Entity<Contract>()
                .HasOne<Employee>(a => a.Employee)
                .WithMany(b => b.Contracts)
                .HasForeignKey(a => a.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);
            */

        }

    }
}
