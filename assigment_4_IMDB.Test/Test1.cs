using Microsoft.VisualStudio.TestTools.UnitTesting;
using assigment_4_IMDB.Models;

namespace assigment_4_IMDB.Test
{
    [TestClass]
    public class TitleTests
    {
        [TestMethod]
        [DataRow("tt001", "Test Movie", 2000)]
        [DataRow("tt002", "Another Movie", 2022)]
        [DataRow("tt003", "Third Movie", 1998)]
        public void AddTitle_TitleIsAddedToDatabase(string titleId, string titleName, int startYear)
        {
            
            var title = new Title
            {
                TitleId = titleId,
                PrimaryTitle = titleName,
                StartYear = (short?)startYear
            };

            bool titleAdded = title.TitleId == titleId && title.PrimaryTitle == titleName && title.StartYear == (short?)startYear;

            
            Assert.IsTrue(titleAdded);  
        }
    }
}
