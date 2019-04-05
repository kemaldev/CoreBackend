using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Configuration
{
    public class HuntingSpotConfiguration: IEntityTypeConfiguration<HuntingSpot> {
        public void Configure(EntityTypeBuilder<HuntingSpot> builder)
        {
            builder.ToTable(nameof(HuntingSpot));
            builder.HasKey (h => h.Id);
            builder.Property (h => h.Id).IsRequired();
        }
    }
}