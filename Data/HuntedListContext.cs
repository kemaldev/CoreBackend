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
            //Many to many relationship between HuntedList and TibiaCharacter.
            modelBuilder.Entity<HuntedListCharacter>()
                .HasKey(hl => new { hl.HuntedListId, hl.TibiaCharacterId });
            modelBuilder.Entity<HuntedListCharacter>()
                .HasOne(hl => hl.HuntedList)
                .WithMany(hl => hl.HuntedListCharacters)
                .HasForeignKey(hl => hl.HuntedListId);
            modelBuilder.Entity<HuntedListCharacter>()
                .HasOne(hl => hl.TibiaCharacter)
                .WithMany(hl => hl.HuntedListCharacters)
                .HasForeignKey(hl => hl.TibiaCharacterId);


            //Many to many relationship between HuntingSpot and TibiaCharacter.
            modelBuilder.Entity<HuntingSpotCharacter>()
                .HasKey(hl => new { hl.HuntingSpotId, hl.TibiaCharacterId });
            modelBuilder.Entity<HuntingSpotCharacter>()
                .HasOne(hl => hl.HuntingSpot)
                .WithMany(hl => hl.HuntingSpotCharacters)
                .HasForeignKey(hl => hl.HuntingSpotId);
            modelBuilder.Entity<HuntingSpotCharacter>()
                .HasOne(hl => hl.TibiaCharacter)
                .WithMany(hl => hl.HuntingSpotCharacters)
                .HasForeignKey(hl => hl.TibiaCharacterId);
        }
    }
}
