

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class GlassCategoryConfiguration : IEntityTypeConfiguration<GlassCategory>
    {
        public void Configure(EntityTypeBuilder<GlassCategory> builder)
        {
           
        }
    }
}
