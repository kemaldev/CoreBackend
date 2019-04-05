using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Configuration
{
    public class TibiaCharacterConfiguration: IEntityTypeConfiguration<TibiaCharacter> {
        public void Configure(EntityTypeBuilder<TibiaCharacter> builder)
        {
            builder.ToTable(nameof(TibiaCharacter));
            builder
                .HasKey (c => c.Id);
            builder.Property(c => c.Id).IsRequired();
        }
    }
}