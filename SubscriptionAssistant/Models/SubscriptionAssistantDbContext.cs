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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany(u => u.Subscriptions)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Subscriptions)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

    
            modelBuilder.Entity<Service>()
                .HasMany(s => s.Subscriptions)
                .WithOne(sub => sub.Service)
                .HasForeignKey(sub => sub.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Subscription>()
                .HasMany(s => s.Payments)
                .WithOne(p => p.Subscription)
                .HasForeignKey(p => p.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Стриминговые сервисы", Description = "Видео и музыкальные платформы" },
                new Category { Id = 2, Name = "Программное обеспечение", Description = "Подписки на ПО и приложения" },
                new Category { Id = 3, Name = "Облачные сервисы", Description = "Хранилища и облачные решения" },
                new Category { Id = 4, Name = "Образование", Description = "Онлайн-курсы и обучающие платформы" },
                new Category { Id = 5, Name = "Игры", Description = "Игровые подписки и сервисы" }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "Netflix", LogoUrl = "/logos/netflix.png", BasePrice = 599m },
                new Service { Id = 2, Name = "Spotify", LogoUrl = "/logos/spotify.png", BasePrice = 299m },
                new Service { Id = 3, Name = "YouTube Premium", LogoUrl = "/logos/youtube.png", BasePrice = 399m },
                new Service { Id = 4, Name = "Microsoft 365", LogoUrl = "/logos/microsoft.png", BasePrice = 799m },
                new Service { Id = 5, Name = "Adobe Creative Cloud", LogoUrl = "/logos/adobe.png", BasePrice = 2499m }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "иванов", Email = "ivanov@example.com", PasswordHash = "hashed_password_1", CreatedAt = DateTime.UtcNow },
                new User { Id = 2, Username = "петров", Email = "petrov@example.com", PasswordHash = "hashed_password_2", CreatedAt = DateTime.UtcNow },
                new User { Id = 3, Username = "сидорова", Email = "sidorova@example.com", PasswordHash = "hashed_password_3", CreatedAt = DateTime.UtcNow },
                new User { Id = 4, Username = "кузнецов", Email = "kuznetsov@example.com", PasswordHash = "hashed_password_4", CreatedAt = DateTime.UtcNow },
                new User { Id = 5, Username = "смирнов", Email = "smirnov@example.com", PasswordHash = "hashed_password_5", CreatedAt = DateTime.UtcNow }
            );

            modelBuilder.Entity<Subscription>().HasData(
                new Subscription { Id = 1, Name = "Мой Netflix Премиум", Price = 599m, StartDate = DateTime.UtcNow.AddMonths(-2), NextPaymentDate = DateTime.UtcNow.AddDays(15), BillingCycle = "ежемесячно", IsActive = true, UserId = 1, CategoryId = 1, ServiceId = 1 },
                new Subscription { Id = 2, Name = "Spotify Премиум", Price = 299m, StartDate = DateTime.UtcNow.AddMonths(-6), NextPaymentDate = DateTime.UtcNow.AddDays(5), BillingCycle = "ежемесячно", IsActive = true, UserId = 1, CategoryId = 1, ServiceId = 2 },
                new Subscription { Id = 3, Name = "YouTube Premium Семейный", Price = 699m, StartDate = DateTime.UtcNow.AddMonths(-1), NextPaymentDate = DateTime.UtcNow.AddDays(25), BillingCycle = "ежемесячно", IsActive = true, UserId = 2, CategoryId = 1, ServiceId = 3 },
                new Subscription { Id = 4, Name = "Microsoft 365 Личный", Price = 799m, StartDate = DateTime.UtcNow.AddYears(-1), NextPaymentDate = DateTime.UtcNow.AddDays(30), BillingCycle = "ежегодно", IsActive = true, UserId = 3, CategoryId = 2, ServiceId = 4 },
                new Subscription { Id = 5, Name = "Adobe Creative Cloud", Price = 2499m, StartDate = DateTime.UtcNow.AddMonths(-3), NextPaymentDate = DateTime.UtcNow.AddDays(10), BillingCycle = "ежемесячно", IsActive = true, UserId = 4, CategoryId = 2, ServiceId = 5 }
            );

            modelBuilder.Entity<Payment>().HasData(
                new Payment { Id = 1, Amount = 599m, PaymentDate = DateTime.UtcNow.AddMonths(-1), IsSuccessful = true, SubscriptionId = 1 },
                new Payment { Id = 2, Amount = 299m, PaymentDate = DateTime.UtcNow.AddMonths(-1), IsSuccessful = true, SubscriptionId = 2 },
                new Payment { Id = 3, Amount = 699m, PaymentDate = DateTime.UtcNow.AddMonths(-1), IsSuccessful = true, SubscriptionId = 3 },
                new Payment { Id = 4, Amount = 799m, PaymentDate = DateTime.UtcNow.AddYears(-1), IsSuccessful = true, SubscriptionId = 4 },
                new Payment { Id = 5, Amount = 2499m, PaymentDate = DateTime.UtcNow.AddMonths(-1), IsSuccessful = true, SubscriptionId = 5 }
            );

            modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "User", Description = "Обычный пользователь" },
            new Role { Id = 2, Name = "Admin", Description = "Администратор" }
            );
        }
    }
}
