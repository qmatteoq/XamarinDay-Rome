using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Azure.Mobile.Server;

namespace TrackSeries.Backend.DataObjects
{
    [Table("FavoriteShow-CSharp")]
    public class FavoriteShow: EntityData
    {
        public int TrackSeriesId { get; set; }

        public bool IsFavorite { get; set; }

        public string UserId { get; set; }
     
    }
}
