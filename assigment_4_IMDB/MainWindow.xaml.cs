using assigment_4_IMDB.ViewModels;
using assigment_4_IMDB.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace assigment_4_IMDB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContentControl.Content = new FeaturedView(); // Default view on startup
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new FeaturedView();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new SearchView();
        }
    }
}