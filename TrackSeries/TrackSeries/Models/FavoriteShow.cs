namespace TrackSeries.Models
{
    public class FavoriteShow
    {
        public string Id { get; set; }

        public int TrackSeriesId { get; set; }

        public bool IsFavorite { get; set; }

        public FavoriteShow(int trackSeriesId)
        {
            TrackSeriesId = trackSeriesId;
        }

        public FavoriteShow()
        {

        }
    }
}
