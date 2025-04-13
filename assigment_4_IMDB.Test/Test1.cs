namespace assigment_4_IMDB.Test
{
    [TestClass]
    public sealed class Test1
    {

        private MainWindow _mainWindow;

        [TestInitialize]
        public void InitializeAllTests()
        {
            _mainWindow = new MainWindow();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
