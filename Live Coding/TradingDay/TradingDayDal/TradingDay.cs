using System.Globalization;
using System.Xml.Linq;

namespace TradingDayDal;

public class TradingDay
{
    public TradingDay()
    {
        
    }
    public TradingDay(XElement tradingDayNode)
    {
        this.Date = DateOnly.Parse(tradingDayNode.Attribute("time").Value);

        //CultureInfo ci = CultureInfo.InvariantCulture; //new CultureInfo("en-US");
        
        NumberFormatInfo nfiEzb = new() { NumberDecimalSeparator = "." };
        
        this.ExchangeRates = tradingDayNode.Elements().Select(
            el => new ExchangeRate()
            {
                Symbol = el.Attribute("currency").Value,
                Rate = Convert.ToDouble(el.Attribute("rate").Value, nfiEzb),
                TradingDay = this
            }).ToList();
    }

    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public List<ExchangeRate> ExchangeRates { get; set; } = null!;

    public string? Location { get; set; }
}

public class ExchangeRate
{
    public int Id { get; set; }
    public TradingDay TradingDay { get; set; }
    public string Symbol { get; set; }
    public double Rate { get; set; }
    public byte[]? Flagsymbol { get; set; }
}