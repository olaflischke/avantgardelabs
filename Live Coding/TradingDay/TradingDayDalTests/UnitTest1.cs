using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TradingDayDal;

namespace TradingDayDalTests;

public class Tests
{
    private Archive archive;
    private string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist.xml";
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void IsArchiveInitializing()
    {
          archive = new Archive(url);

          ExchangeRate usd = archive.TradingDays?.FirstOrDefault()?.ExchangeRates?.FirstOrDefault();
          Console.WriteLine($"{usd.Symbol}: {usd.Rate}");
        
        Assert.AreEqual(62, archive.TradingDays.Count);
    }

    [Test]
    public void IsArchiveSaving()
    {
        archive = new Archive(url);
        archive.SaveToDb();
        
        Assert.Pass();
    }

    [Test]
    public void RankUsd()
    {
        TradingDayContext context = new();

        Stopwatch stopwatch = new();

        stopwatch.Start();
        
        var qRates = context.TradingDays
            .SelectMany(day => day.ExchangeRates)
            .AsNoTracking()
            .AsEnumerable()
            .Select((rate, i) => new { Rate = rate, Index = i })
            .Where(el => el.Rate.Symbol == "USD")
            .ToList();

        stopwatch.Stop();
        
        Console.WriteLine($"{stopwatch.Elapsed:g}");
        
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"{qRates[i].Index}: {qRates[i].Rate.Symbol} - {qRates[i].Rate.Rate}");
        }
    }
    
    
}