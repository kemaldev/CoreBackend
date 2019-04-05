using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class HuntedListContext : DbContext
    {
        public DbSet<HuntedList> HuntedList { get; set; }
        public DbSet<TibiaCharacter> TibiaCharacter { get; set; }
        public DbSet<HuntingSpot> HuntingSpot { get; set; }

        public HuntedListContext(DbContextOptions<HuntedListContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HuntedListContext).Assembly);
        }
    }
}
