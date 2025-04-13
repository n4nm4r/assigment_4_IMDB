using Microsoft.VisualStudio.TestTools.UnitTesting;
using assigment_4_IMDB.Models;
using System.Collections.Generic;
using System.Linq;
using assigment_4_IMDB.Views;
using System.Globalization;

namespace assigment_4_IMDB.Test
{
    [TestClass]
    public class TitleTests
    {
        [TestMethod]
        [DataRow("tt001", "Test Movie", 2000)]
        [DataRow("tt002", "Another Movie", 2022)]
        [DataRow("tt003", "Third Movie", 1998)]
        public void CreateTitle_TitlePropertiesAreSetCorrectly(string titleId, string titleName, int startYear)
        {
            var title = new Title
            {
                TitleId = titleId,
                PrimaryTitle = titleName,
                StartYear = (short?)startYear
            };

            Assert.AreEqual(titleId, title.TitleId);
            Assert.AreEqual(titleName, title.PrimaryTitle);
            Assert.AreEqual((short?)startYear, title.StartYear);
        }

        [TestMethod]
        public void FilterTitles_ByRatingAboveThreshold_ReturnsCorrectTitles()
        {
            var titles = new List<Title>
            {
                new Title { PrimaryTitle = "Low Rated", Rating = new Rating { AverageRating = 4.0m } },
                new Title { PrimaryTitle = "High Rated", Rating = new Rating { AverageRating = 8.5m } },
                new Title { PrimaryTitle = "Borderline", Rating = new Rating { AverageRating = 7.0m } }
            };

            decimal threshold = 7.0m;

            var filtered = titles.Where(t => t.Rating != null && t.Rating.AverageRating >= threshold).ToList();

            Assert.AreEqual(2, filtered.Count);
            Assert.IsTrue(filtered.Any(t => t.PrimaryTitle == "High Rated"));
            Assert.IsTrue(filtered.Any(t => t.PrimaryTitle == "Borderline"));
        }

        [TestMethod]
        public void FilterTitles_ByStartYear_ReturnsCorrectTitles()
        {
            var titles = new List<Title>
            {
                new Title { PrimaryTitle = "Old Movie", StartYear = 1995 },
                new Title { PrimaryTitle = "Recent Movie", StartYear = 2021 },
                new Title { PrimaryTitle = "Modern Classic", StartYear = 2010 }
            };

            short yearThreshold = 2000;

            var filtered = titles.Where(t => t.StartYear >= yearThreshold).ToList();

            Assert.AreEqual(2, filtered.Count);
            Assert.IsTrue(filtered.Any(t => t.PrimaryTitle == "Recent Movie"));
            Assert.IsTrue(filtered.Any(t => t.PrimaryTitle == "Modern Classic"));
        }

        [TestMethod]
        public void FilterTitles_ByKeywordInTitle_ReturnsMatchingTitles()
        {
            var titles = new List<Title>
            {
                new Title { PrimaryTitle = "The Great Adventure" },
                new Title { PrimaryTitle = "Adventure Time" },
                new Title { PrimaryTitle = "Romantic Comedy" }
            };

            string keyword = "adventure";

            var filtered = titles.Where(t => t.PrimaryTitle.ToLower().Contains(keyword.ToLower())).ToList();

            Assert.AreEqual(2, filtered.Count);
            Assert.IsTrue(filtered.Any(t => t.PrimaryTitle == "The Great Adventure"));
            Assert.IsTrue(filtered.Any(t => t.PrimaryTitle == "Adventure Time"));
        }
    }

    [TestClass]
    public class StarConverterTests
    {
        [TestMethod]
        [DataRow(3.0, 3)]
        [DataRow(4.9, 4)]
        [DataRow(0.0, 0)]
        [DataRow(5.1, 5)]
        public void Convert_ShouldReturnCorrectNumberOfStars(double rating, int expectedStarCount)
        {
            var converter = new StarConverter();

            var result = converter.Convert((decimal)rating, null, null, CultureInfo.InvariantCulture) as List<string>;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStarCount, result.Count);
            Assert.IsTrue(result.All(s => s == "★"));
        }


        [TestMethod]
        [ExpectedException(typeof(System.NotImplementedException))]
        public void ConvertBack_ShouldThrowNotImplementedException()
        {
            var converter = new StarConverter();
            converter.ConvertBack(null, null, null, CultureInfo.InvariantCulture);
        }
    }
}
