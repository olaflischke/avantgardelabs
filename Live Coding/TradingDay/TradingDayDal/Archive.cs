using System.Xml.Linq;

namespace TradingDayDal;

public class Archive
{
    public Archive(string url)
    {
        this.TradingDays = GetData(url);
        //SaveToDb();
    }

    /// <summary>
    /// Saves the TradingDayList to a local Database
    /// </summary>
    public void SaveToDb()
    {
        TradingDayContext context = new();
        
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        context.TradingDays.AddRange(TradingDays);
        
        context.SaveChanges();
    }
    

    private List<TradingDay>? GetData(string url)
    {
        XDocument xml = XDocument.Load(url);

        var qDays = xml.Root.Descendants()
            .Where(xe => xe.Name.LocalName == "Cube" && xe.Attributes().Any(at => at.Name == "time"))
            .Select(xe => new TradingDay(xe) );

        return qDays.ToList();
    }

    public List<TradingDay> TradingDays { get; set; }
}