using Microsoft.EntityFrameworkCore;
using SubscriptionAssistant.Models;
using SubscriptionAssistant.Models.SubscriptionManager.Models;


namespace SubscriptionAssistant.Models
{
    public class SubscriptionAssistantDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public SubscriptionAssistantDbContext(DbContextOptions<SubscriptionAssistantDbContext> options)
            : base(options) { }
    }
}
