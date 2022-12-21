using Microsoft.EntityFrameworkCore;
using loremipsum.Entities;

namespace loremipsum.Persistence
{
    public class GymContext: DbContext
    {
        public virtual DbSet<Member> Members { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Contract> Contracts { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Checkout> Checkouts { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Dispencer> Dispencers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = Gym;Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Member>()
                .HasOne<Contract>(a => a.Contract)
                .WithOne(b => b.Member)
                .HasForeignKey(a => a.MemberID)
                .OnDelete(DeleteBehavior.Cascade);

                 modelBuilder.Entity<Contract>()
                .HasOne<Checkout>(a => a.Checkout)
                .WithOne(b => b.Contract)
                .HasForeignKey(a => a.ContractID)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Employee>()
                .HasMany<>(a => a.Contracts)
                .WithOne(b => b.Employee)
                .HasForeignKey(a => a.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Checkout>()
                .HasMany<>(a => a.Orders)
                .WithOne(b => b.Checkout)
                .HasForeignKey(a => a.CheckoutID)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Order>()
                .HasOne<>(a => a.Member)
                .WithMany(b => b.Orders)
                .HasForeignKey(a => a.OrderID)
                .OnDelete(DeleteBehavior.Cascade);
            
                modelBuilder.Dispencer<>()
                .HasMany<>(a => a.Orders)
                .WithMany(b => b.Dispencers)
                .HasForeignKey(a => a.DispencerID)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Article>()
                .HasMany<>(a => a.Dispencers)
                .WithMany(b => b.Articles)
                .HasForeignKey(a => a.ArticleID)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
