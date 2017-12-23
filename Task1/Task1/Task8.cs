using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task1
{
    [TestClass]
    public class Task8
    {
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string ADMIN_URL = "http://localhost/litecart";
        private const int WAIT_TIMEOUT = 10;
        private const string EXPECTED_TITLE = "Online Store | My Store";

        private IWebDriver driver;
        private WebDriverWait wait;

        [TestInitialize()]
        public void Task8_Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(ADMIN_URL);
            wait.Until(ExpectedConditions.TitleIs(EXPECTED_TITLE));
        }

        [TestMethod]
        public void Task8_CheckStickers()
        {
            ReadOnlyCollection<IWebElement> products = driver.FindElements(By.XPath(".//li[contains(@class,'product')]"));
            foreach (var product in products)
            {
                var stickerElement = product.FindElement(By.XPath(".//div[contains(@class,'sticker')]"));
                Assert.IsTrue(stickerElement.Displayed);
            }
        }

        [TestCleanup()]
        public void Task8_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
