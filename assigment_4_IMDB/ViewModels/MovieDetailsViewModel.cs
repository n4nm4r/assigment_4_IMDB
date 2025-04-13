using assigment_4_IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assigment_4_IMDB.ViewModels
{
    public class MovieDetailsViewModel
    {
        public string TitleName { get; set; }
        public int Year { get; set; }
        public List<int> StarRating { get; set; }

        public MovieDetailsViewModel(Title selectedTitle) : base()
        {
            TitleName = selectedTitle.PrimaryTitle;
            Year = selectedTitle.StartYear ?? 0;

            var avgRating = (decimal)(selectedTitle.Rating?.AverageRating ?? 0);
            StarRating = Enumerable.Range(1, (int)Math.Round(avgRating)).ToList();
        }

    }
}
