using Microsoft.EntityFrameworkCore;
using loremipsum.Gym.Entities;

namespace loremipsum.Gym.Persistence
{
    public class GymContext : DbContext
    {
        public virtual DbSet<Member> Members { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Contract> Contracts { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Order> Orders { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = Gym;Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
            .HasOne<Contract>(a => a.Contract)
            .WithMany(b => b.Members)
            .HasForeignKey(a => a.ContractID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
            .HasOne<Member>(a => a.Member)
            .WithMany(b => b.Orders)
            .HasForeignKey(a =>a.MemberID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
            .HasOne(a => a.Article)
            .WithMany(b =>b.Orders)
            .HasForeignKey(a => a.ArticleID)
            .OnDelete(DeleteBehavior.Cascade);


        }

    }
}
