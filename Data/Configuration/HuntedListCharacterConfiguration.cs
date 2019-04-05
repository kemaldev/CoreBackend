using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Configuration
{
    public class HuntedListCharacterConfiguration: IEntityTypeConfiguration<HuntedListCharacter> {
        public void Configure(EntityTypeBuilder<HuntedListCharacter> builder)
        {
            builder.ToTable(nameof(HuntedListCharacter));

            builder
                .HasKey(hl => new { hl.HuntedListId, hl.TibiaCharacterId });
        }
    }
}