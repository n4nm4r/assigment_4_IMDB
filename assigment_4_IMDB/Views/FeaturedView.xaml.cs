using assigment_4_IMDB.Data;
using assigment_4_IMDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace assigment_4_IMDB.Views
{
    /// <summary>
    /// Interaction logic for FeaturedView.xaml
    /// </summary>
    public partial class FeaturedView : UserControl
    {
        private ImdbContext _imdbContext = new();
        public FeaturedView()
        {
            InitializeComponent();

            
            _imdbContext.Titles.Load();

            var titleQuery =
                from title in _imdbContext.Titles
                join rating in _imdbContext.Ratings on title.TitleId equals rating.TitleId
                where title.TitleType == "movie" && title.PrimaryTitle == "Hamlet"
                select new
                {
                    Title = title.PrimaryTitle,
                    Rating = rating.AverageRating
                };

            FeaturedMovies.ItemsSource = titleQuery.ToList();
        }
    }
}
