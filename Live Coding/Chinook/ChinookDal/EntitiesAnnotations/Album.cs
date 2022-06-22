using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChinookDal.EntitiesAnnotations
{
    [Table("Album")]
    [Index("ArtistId", Name = "IFK_AlbumArtistId")]
    public partial class Album
    {
        public Album()
        {
            Tracks = new HashSet<Track>();
        }

        [Key]
        public int AlbumId { get; set; }
        [StringLength(160)]
        public string Title { get; set; } = null!;
        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        [InverseProperty("Albums")]
        public virtual Artist Artist { get; set; } = null!;
        [InverseProperty("Album")]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
