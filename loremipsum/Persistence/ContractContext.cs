using Microsoft.EntityFrameworkCore;
using loremipsum.Entities;

namespace loremipsum.Persistence
{
    public class ContractContext : DbContext
    {

        public virtual DbSet<Employee> Contracts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = Contracts;Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
                
            */

        }




    }
}
