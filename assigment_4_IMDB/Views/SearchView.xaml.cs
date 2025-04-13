using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using assigment_4_IMDB.Data;
using assigment_4_IMDB.Models;
using assigment_4_IMDB.Services;
using assigment_4_IMDB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace assigment_4_IMDB.Views
{
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
        }

        private string _lastSearchTerm = "";

        public void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string? selectedType = (TypeFilterComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string? selectedCategory = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string currentSearchTerm = SearchTextBox.Text.Trim().ToLower();

            // Guard clause: no search term
            if (string.IsNullOrWhiteSpace(currentSearchTerm))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            

            _lastSearchTerm = currentSearchTerm;

            using var context = new ImdbContext();

            var query = BuildQuery(context, currentSearchTerm, selectedType, selectedCategory);
            var results = query.Take(100).ToList();

            if (results.Count == 0)
            {
                MessageBox.Show("No results found.");
            }

            ResultsListBox.ItemsSource = results;
            System.Diagnostics.Debug.WriteLine($"Search for '{currentSearchTerm}' returned {results.Count} results.");

            //more testing data
            var debugCount = context.Titles
            .GroupBy(t => t.TitleType)
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .ToList();

            foreach (var group in debugCount)
            {
                System.Diagnostics.Debug.WriteLine($"{group.Type}: {group.Count}");
            }
        }

        private IQueryable<Title> BuildQuery(ImdbContext context, string searchTerm, string? selectedType, string? selectedCategory)
        {
            var searchService = new SearchService(context);
            return searchService.BuildQuery(searchTerm, selectedType, selectedCategory);
        }



        private void TitleTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var clickedTitle = textBlock?.Tag as Title;

            if (clickedTitle != null && clickedTitle.TitleType == "tvSeries")
            {
                var seriesDetailsView = new SeriesDetailsView(clickedTitle);

                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainContentControl.Content = seriesDetailsView;
            }
            else if (clickedTitle != null && clickedTitle.TitleType == "movie")
            {
                var movieDetailsView = new MoviesDetailsView(clickedTitle);

                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainContentControl.Content = movieDetailsView;
            }
        }


    }




    
}
