

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HandmadeByDoniApp.Data.Configuration
{
    public class SetConfiguration : IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> builder)
        {
            builder.Property(h => h.CreatedOn)
            .HasDefaultValueSql("GETDATE()");

            builder.Property(h => h.IsActive)
                .HasDefaultValue(true);
        }
    }
}
