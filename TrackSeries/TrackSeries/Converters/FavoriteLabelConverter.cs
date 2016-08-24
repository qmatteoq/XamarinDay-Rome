using System;
using System.Globalization;
using Xamarin.Forms;

namespace TrackSeries.Converters
{
    public class FavoriteLabelConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isFavorite = (bool) value;
            return isFavorite ? "Remove from favorites" : "Add to favorites";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
