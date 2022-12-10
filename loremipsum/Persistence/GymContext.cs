using Microsoft.EntityFrameworkCore;
using loremipsum.Entities;

namespace loremipsum.Persistence
{
    public class GymContext: DbContext
    {
        public virtual DbSet<Member> Members { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Contract> Contracts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = Gym;Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
                modelBuilder.Entity<Member>()
                .HasOne<Contract>(a => a.Contract)
                .WithMany(b => b.Members)
                .HasForeignKey(a => a.MemberID)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Contract>()
                .HasOne<Employee>(a => a.Employee)
                .WithMany(b => b.Contracts)
                .HasForeignKey(a => a.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);
            */

        }

    }
}
