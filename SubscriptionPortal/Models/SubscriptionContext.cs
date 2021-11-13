using Microsoft.EntityFrameworkCore;

namespace SubscriptionPortal.Models
{
    public class SubscriptionPortalContext : DbContext
    {
        public SubscriptionPortalContext(DbContextOptions<SubscriptionPortalContext> options) : base(options)
        {

        }

        public DbSet<Emp> Employee { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
    }
}
