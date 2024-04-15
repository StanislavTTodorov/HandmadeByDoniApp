

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Net.WebRequestMethods;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class GlassConfiguration : IEntityTypeConfiguration<Glass>
    {
        public void Configure(EntityTypeBuilder<Glass> builder)
        {
            builder.Property(h => h.CreatedOn)
                   .HasDefaultValueSql("GETDATE()");

        }
    }
}
