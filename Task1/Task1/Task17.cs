using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace Task1
{
    [TestClass]
    public class Task17
    {
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string URL = "http://localhost/litecart/admin";
        private const int WAIT_TIMEOUT = 10;
        private const string EXPECTED_TITLE = "Online Store | My Store";
        private const string BROWSER = "Chrome";
        private IWebDriver driver;
        private static WebDriverWait wait;
        private string PRODUCT_GROUP = "Unisex";


        private void BrowserSetup()
        {
            if (BROWSER == "Chrome")
            {
                driver = new ChromeDriver();
                return;
            }
            if (BROWSER == "Firefox")
            {
                driver = new FirefoxDriver();
                return;
            }
            if (BROWSER == "IE")
            {
                driver = new InternetExplorerDriver();
                return;
            }

            Assert.Fail("Browser does not supported");
        }
        [TestInitialize()]
        public void Task17_Setup()
        {
            BrowserSetup();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
        }

        [TestMethod]
        public void Task17_Case_CheckBrowserLogs()
        {
            //Login

            var loginField = this.driver.FindElement(By.XPath(".//input[@name='username']"));
            var pwdField = this.driver.FindElement(By.XPath(".//input[@name='password']"));
            var loginBtn = this.driver.FindElement(By.XPath(".//button[@name='login']"));

            loginField.Clear();
            loginField.SendKeys(ADMIN_LOGIN);

            pwdField.Clear();
            pwdField.SendKeys(ADMIN_PASSWORD);

            loginBtn.Click();

            //Go to Catalog => Select Category with products
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");

            var msgs = new Dictionary<string, IReadOnlyCollection<LogEntry>>();

            //Get all products names
            var products = driver.FindElements(
                    By.XPath(".//table[contains(@class,'dataTable')]//td/a[contains(@href,'product_id')]"))
                .Select(prod => prod.Text).ToList();

            //use this for delete links referring to edit product

            products.RemoveAll(s => s.Equals(""));

            foreach (var product in products)
            {
                driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
                driver.FindElement(By.XPath($".//table[@class='dataTable']//td/img/../a[contains(text(),'{product}')]")).Click();
                var logs = driver.Manage().Logs.GetLog("browser");
                if (!logs.Count.Equals(0))
                {
                    msgs.Add(product, logs);
                }
            }
            if (msgs.Count.Equals(0)) return;
            //Output our messages
            foreach (var msg in msgs)
            {
                Console.WriteLine($"\nFounded errors for product - {msg.Key}\n");
                foreach (var logMsg in msg.Value)
                {
                    Console.WriteLine($"[ {logMsg.Level} ] | [ {logMsg.Message} ] ");
                }
            }
            //If we got messages -> fail test
            Assert.IsTrue(msgs.Count.Equals(0));
        }

        [TestCleanup()]
        public void Task17_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
