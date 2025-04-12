using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using assigment_4_IMDB.Data;
using assigment_4_IMDB.Models;
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
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

            //// Prevent repeated identical search
            //if (currentSearchTerm == _lastSearchTerm)
            //{
            //    System.Diagnostics.Debug.WriteLine("Search term hasn't changed. Skipping search.");
            //    return;
            //}

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
            var query = context.Titles
                .Include(t => t.Rating)
                .Include(t => t.Genres)
                .Include(t => t.Names)
                .AsQueryable();

            // Type filter
            if (selectedType == "Movies")
                query = query.Where(t => t.TitleType == "movie");
            else if (selectedType == "Series")
                query = query.Where(t => t.TitleType == "tvSeries" && t.EpisodeParentTitles.Any());

            // Category filter
            switch (selectedCategory)
            {
                case "Title":
                    query = query.Where(t => t.PrimaryTitle != null &&
                        EF.Functions.Like(t.PrimaryTitle.ToLower(), $"%{searchTerm}%"));
                    break;

                case "Genre":
                    query = query.Where(t => t.Genres.Any(g =>
                        EF.Functions.Like(g.Name.ToLower(), $"%{searchTerm}%")));
                    break;

                case "Actor":
                    query = query.Where(t => t.Names.Any(n =>
                        EF.Functions.Like(n.PrimaryName.ToLower(), $"%{searchTerm}%")));
                    break;

                case "Rating":
                    if (decimal.TryParse(searchTerm, out decimal rating))
                    {
                        query = query.Where(t => t.Rating != null && t.Rating.AverageRating >= rating);
                    }
                    break;

                case "Year":
                    if (int.TryParse(searchTerm, out int year))
                    {
                        query = query.Where(t => t.StartYear == year);
                    }
                    break;

                default:
                    MessageBox.Show("Please select a valid category.");
                    break;
            }

            return query;
        }
    }
}
