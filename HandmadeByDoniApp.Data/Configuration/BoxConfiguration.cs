

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class BoxConfiguration : IEntityTypeConfiguration<Box>
    {
        public void Configure(EntityTypeBuilder<Box> builder)
        {
            builder.Property(h => h.CreateOn)
                 .HasDefaultValueSql("GETDATE()");

            builder.Property(h => h.IsActive)
                   .HasDefaultValue(true);

        }
    }
}
