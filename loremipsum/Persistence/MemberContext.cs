using Microsoft.EntityFrameworkCore;
using loremipsum.Entities;

namespace loremipsum.Persistence
{
    public class MemberContext: DbContext
    {
        public virtual DbSet<Member> Members { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = Members;Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
                modelBuilder.Entity<Member>()
                .HasOne<Contract>(a => a.Contract)
                .WithMany(b => b.Members)
                .HasForeignKey(a => a.MemberID)
                .OnDelete(DeleteBehavior.Cascade);
            */

        }

    }
}
