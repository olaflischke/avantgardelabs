using ChinookDal.Entities;
using NUnit.Framework.Internal;

namespace ChinookDalTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        ChinookContext context = new ChinookContext();

        List<Genre> genres = context.Genres.ToList();
        
        foreach (Genre genre in genres)
        {
            Thread.Sleep(100);
            
            Console.WriteLine("-------------");
            Console.WriteLine(genre.Name);
            Console.WriteLine("-------------");

            var qArtists = context.Tracks.Where(tr => tr.Genre==genre)
                .Select(tr => tr.Album.Artist.Name)
                .Distinct();

            foreach (string artist in qArtists)
            {
                Console.WriteLine(artist);
            }
        }
        
        
    }

    [Test]
    public void ArtistsByGenre()
    {
        ChinookContext context = new ChinookContext();
        context.Log = LogIt;

        var qArtists = context.Genres.Select(genre => new
        {
           Genre = genre.Name,
           Artists = context.Tracks.Where(tr => tr.Genre == genre)
                .Select(tr => tr.Album.Artist.Name)
                .Distinct().AsEnumerable()
        });

        foreach (var info in qArtists)
        {
            Console.WriteLine("-------------");
            Console.WriteLine(info.Genre);
            Console.WriteLine("-------------");

            foreach (string? artist in info.Artists)
            {
                Console.WriteLine(artist);
            }
            
        }
    }

    private void LogIt(string logString)
    {
        Console.WriteLine(logString);
    }
}