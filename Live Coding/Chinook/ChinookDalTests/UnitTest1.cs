using ChinookDal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;

namespace ChinookDalTests;

public class Tests
{
    private DbContextOptions<ChinookContext> options;

    [SetUp]
    public void Setup()
    {
        options = new DbContextOptionsBuilder<ChinookContext>()
            .UseNpgsql("server=localhost;port=5432;database=chinook;user id=demo;password=Geheim123")
            .LogTo(log => LogIt(log), LogLevel.Information)
            .Options;
    }

    [Test]
    public void Test1()
    {
        ChinookContext context = new ChinookContext(options);

        List<Genre> genres = context.Genres.ToList();

        foreach (Genre genre in genres)
        {
            Thread.Sleep(100);

            Console.WriteLine("-------------");
            Console.WriteLine(genre.Name);
            Console.WriteLine("-------------");

            var qArtists = context.Tracks.Where(tr => tr.Genre == genre)
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
        ChinookContext context = new ChinookContext(options);
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

    [Test]
    public void AddArtist()
    {
        Artist artist = new Artist() { Name = "Bach", ArtistId = 1002 };


        using (ChinookContext context = new ChinookContext(options))
        {
            context.Artists.Add(artist);
            AddAlbumForArtist(context, artist, "Die Arnstädter Orgel");

            context.SaveChanges();
        }
    }


    public void AddAlbumForArtist(ChinookContext context, Artist artist, string? albumtitle = null)
    {
        if (albumtitle != null)
        {
            // using (ChinookContext context = new ChinookContext(options))
            // {
            Album album = new Album() { Title = albumtitle, Artist = artist, AlbumId = 1002 };
            context.Albums.Add(album);

            // context.SaveChanges();
            // }
        }
        //artist.Albums.Add(album);
    }

    private void LogIt(string logString)
    {
        Console.WriteLine(logString);
    }
}