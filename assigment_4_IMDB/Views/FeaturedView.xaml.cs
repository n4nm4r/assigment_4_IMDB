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
        public ObservableCollection<Title> TitleList { get; set; }
        public FeaturedView()
        {
            InitializeComponent();
            _imdbContext.Titles.Load();

            TitleList = _imdbContext.Titles.Local.ToObservableCollection();

            // Causes program to keep loading garbage data?? Works fine and loads data without.
            //FeaturedMovies.ItemsSource = TitleList;
        }
    }
}
