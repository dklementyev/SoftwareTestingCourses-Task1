using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace Task1
{
    [TestClass()]
    public class Task1Test
    {
        #region Constants
        private const int WAIT_TIMEOUT = 10;
        private const string URL = "https://google.ru/";
        private const string EXPECTED_TITLE = "Google";
        #endregion

        private IWebDriver driver;
        private WebDriverWait wait;
        
        [TestInitialize()]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
        }
        [TestMethod()]
        public void TestCase()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(WAIT_TIMEOUT);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
            wait.Until(ExpectedConditions.TitleIs(EXPECTED_TITLE));
        }

        [TestCleanup()]
        public void TestCleanUp()
        {
            driver.Quit();
            driver = null;
        }
    }
}
