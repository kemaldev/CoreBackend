using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data {
    public class HuntedListConfiguration : IEntityTypeConfiguration<HuntedList> {
        public void Configure(EntityTypeBuilder<HuntedList> builder)
        {
            builder.ToTable(nameof(HuntedList));
            builder.HasKey (h => h.Id);
            builder.Property(h => h.Id).IsRequired();
        }
    }
}