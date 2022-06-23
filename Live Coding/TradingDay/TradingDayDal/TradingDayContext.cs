using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace TradingDayDal;

public class TradingDayContext:DbContext
{
    public DbSet<TradingDay> TradingDays { get; set; } = null!;
    public DbSet<ExchangeRate> ExchangeRates { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "server=localhost;port=5432;database=TradingDayDb;user id=demo;password=Geheim123");
        }
    }
}