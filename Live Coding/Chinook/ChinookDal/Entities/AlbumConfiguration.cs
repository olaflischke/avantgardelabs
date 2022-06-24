using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChinookDal.Entities;

public class AlbumConfiguration:IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
            builder.ToTable("Album");

            builder.HasIndex(e => e.ArtistId, "IFK_AlbumArtistId");

            builder.Property(e => e.AlbumId).ValueGeneratedNever();

            builder.Property(e => e.Title).HasMaxLength(160);

            // builder.HasOne(d => d.Artist)
            //     .WithMany(p => p.Albums)
            //     .HasForeignKey(d => d.ArtistId)
            //     .OnDelete(DeleteBehavior.ClientSetNull)
            //     .HasConstraintName("FK_AlbumArtistId");
            //     
            // // Global Query Filter
            // builder.HasQueryFilter(al => al.Deleted == 'n');
            // // Methode IgnoreQueryFilters() überschreibt diese Einstellung
            //
            // // Value-Converter
            // var charBoolConverter = new ValueConverter<char, bool>(
            //     s => (s == 'y' ? true : false),   // Convert Objekt -> Datenbank
            //     s => (s == true ? 'y': 'n')     // ConvertBack Datenbank -> Objekt
            //     );
            //
            // builder.Property(e => e.Deleted).HasConversion(charBoolConverter);

            builder.Ignore(al => al.Deleted);
    }
}