

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class DecanterConfiguration : IEntityTypeConfiguration<Decanter>
    {
        public void Configure(EntityTypeBuilder<Decanter> builder)
        {
            builder.Property(h => h.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(h => h.IsActive)
                   .HasDefaultValue(true);

            builder.Property(h => h.IsSet)
                   .HasDefaultValue(false);
        }
    }
}
