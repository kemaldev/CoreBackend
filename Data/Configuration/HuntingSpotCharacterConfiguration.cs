using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Configuration
{
    public class HuntingSpotCharacterConfiguration: IEntityTypeConfiguration<HuntingSpotCharacter> {
        public void Configure(EntityTypeBuilder<HuntingSpotCharacter> builder)
        {
            builder.ToTable(nameof(HuntingSpotCharacter));
            
            builder
                .HasKey(hl => new { hl.HuntingSpotId, hl.TibiaCharacterId });
        }
    }
}