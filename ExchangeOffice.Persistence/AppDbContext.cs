using System;
using ExchangeOffice.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeOffice.Persistence
{
    public class AppDbContext: DbContext
    {
        public virtual DbSet<Currency> Currencies { get; set; }

        public virtual DbSet<ExchangeRate> CurrencyExchangeRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=exchange-rates;user=root;password=Hello12345!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.ExchangeRates);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Nominal).IsRequired();
                entity.Property(e => e.CharCode).IsRequired();
            });
            
            modelBuilder.Entity<ExchangeRate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Currency);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.SellPrice).IsRequired();
                entity.Property(e => e.BuyPrice).IsRequired();
            });
        }
    }
}