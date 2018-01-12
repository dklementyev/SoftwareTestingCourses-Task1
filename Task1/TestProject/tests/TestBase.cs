using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.app;

namespace TestProject.tests
{
    public class TestBase
    {
        public Application app;

        [TestInitialize]
        public void Setup()
        {
            app = new Application();
        }

        [TestCleanup]
        public void TearDown()
        {
            app.Quit();
            app = null;
        }
    }
}
