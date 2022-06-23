using TradingDayDal;

namespace TradingDayDalTests;

public class Tests
{
    private Archive archive;
    private string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";
    
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
}