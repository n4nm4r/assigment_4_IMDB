using assigment_4_IMDB.Data;
using assigment_4_IMDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace assigment_4_IMDB.Services
{
    public class SearchService
    {
        private readonly ImdbContext _context;

        public SearchService(ImdbContext context)
        {
            _context = context;
        }

        public IQueryable<Title> BuildQuery(string searchTerm, string? selectedType, string? selectedCategory)
        {
            var query = _context.Titles
                .Include(t => t.Rating)
                .Include(t => t.Genres)
                .AsQueryable();

            if (selectedType == "Movies")
                query = query.Where(t => t.TitleType == "movie");
            else if (selectedType == "Series")
                query = query.Where(t => t.TitleType == "tvSeries" && t.EpisodeParentTitles.Any());

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
            }

            return query;
        }
    }
}
