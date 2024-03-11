

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration
{
    internal class GlassConfiguration : IEntityTypeConfiguration<Glass>
    {
        public void Configure(EntityTypeBuilder<Glass> builder)
        {
            builder.Property(h => h.CreateOn)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(h => h.IsActive)
                   .HasDefaultValue(true);

            builder.Property(h => h.IsSet)
                   .HasDefaultValue(false);
        }
    }
}
