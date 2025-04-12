using assigment_4_IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assigment_4_IMDB.ViewModels
{
    public class SeriesDetailsViewModel
    {
        public string TitleName { get; set; }
        public int Year { get; set; }
        public List<string> ActorNames { get; set; }
        public List<int> RatingStars { get; set; }  // Used for binding stars
        public List<Episode> Episodes { get; set; }

        public SeriesDetailsViewModel(Title selectedTitle)
        {
            TitleName = selectedTitle.PrimaryTitle;
            Year = selectedTitle.StartYear ?? 0;
            ActorNames = selectedTitle.Names.Select(n => n.PrimaryName).ToList();

            var avgRating = selectedTitle.Rating?.AverageRating ?? 0;
            RatingStars = Enumerable.Range(1, (int)Math.Round(avgRating)).ToList();

            Episodes = selectedTitle.EpisodeParentTitles
                .OrderBy(e => e.SeasonNumber)
                .ThenBy(e => e.EpisodeNumber)
                .ToList();
        }




    }


}
