using Microsoft.EntityFrameworkCore;
using MenuOrder.Models;

namespace MenuOrder.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Настройка подключения к базе данных SQLite
            optionsBuilder.UseSqlite("Data Source=menuorder.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация для правильного наследования
            modelBuilder.Entity<Dish>().ToTable("Dishes");
            modelBuilder.Entity<Beverage>().ToTable("Beverages");

            // Конфигурация для Order
            modelBuilder.Entity<Order>(entity =>
            {
                // Настройка свойства ItemsJson для хранения в базе данных
                entity.Property(e => e.ItemsJson)
                    .HasColumnName("Items")
                    .HasConversion(
                        v => v,
                        v => string.IsNullOrEmpty(v) ? "[]" : v
                    );
                // Настройка типа данных для TotalPrice
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");
            });
        }
    }
}