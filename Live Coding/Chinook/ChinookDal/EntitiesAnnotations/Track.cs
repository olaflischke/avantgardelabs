﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChinookDal.EntitiesAnnotations
{
    [Table("Track")]
    [Index("AlbumId", Name = "IFK_TrackAlbumId")]
    [Index("GenreId", Name = "IFK_TrackGenreId")]
    [Index("MediaTypeId", Name = "IFK_TrackMediaTypeId")]
    public partial class Track
    {
        [Key]
        public int TrackId { get; set; }
        [StringLength(200)]
        public string Name { get; set; } = null!;
        public int? AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }
        [StringLength(220)]
        public string? Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        [Precision(10, 2)]
        public decimal UnitPrice { get; set; }

        [ForeignKey("AlbumId")]
        [InverseProperty("Tracks")]
        public virtual Album? Album { get; set; }
        [ForeignKey("GenreId")]
        [InverseProperty("Tracks")]
        public virtual Genre? Genre { get; set; }
    }
}
