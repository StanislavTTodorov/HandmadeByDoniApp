using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Data
{
    public class HandmadeByDoniAppDbContext : IdentityDbContext
    {
        public HandmadeByDoniAppDbContext(DbContextOptions<HandmadeByDoniAppDbContext> options)
            : base(options)
        {
        }
    }
}