using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace assigment_4_IMDB.Views
{
    public class StarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal rating = (decimal)value;
            var stars = new List<string>();

            for (int i = 0; i < Math.Floor(rating); i++) 
            {
                stars.Add("★");
            }

            // Debugging: Log the result
            System.Diagnostics.Debug.WriteLine($"StarConverter received: {rating}, returning {string.Join(", ", stars)}");

            return stars;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}