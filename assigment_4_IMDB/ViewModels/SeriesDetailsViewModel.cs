using assigment_4_IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace assigment_4_IMDB.ViewModels
{
    public class SeriesDetailsViewModel
    {
        public string TitleName { get; set; }
        public int Year { get; set; }

        // Stars to display (e.g., 7 means 7 filled stars)
        public List<int> RatingStars { get; set; }

        // List of episodes for this series
        public List<Episode> Episodes { get; set; }

        public SeriesDetailsViewModel(Title selectedTitle)
        {
            // Title and year
            TitleName = selectedTitle.PrimaryTitle;
            Year = selectedTitle.StartYear ?? 0;

            // Rating stars
            var avgRating = selectedTitle.Rating?.AverageRating ?? 0;
            RatingStars = Enumerable.Range(1, (int)Math.Round(avgRating)).ToList();

            // Episodes ordered by season and episode number
            Episodes = selectedTitle.EpisodeParentTitles
                .OrderBy(e => e.SeasonNumber)
                .ThenBy(e => e.EpisodeNumber)
                .ToList();
        }
    }
}
