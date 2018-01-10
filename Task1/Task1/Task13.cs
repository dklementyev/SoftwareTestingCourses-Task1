using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System.Threading;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Task1
{
    [TestClass]
    public class Task13
    {
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string URL = "http://localhost/litecart";
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
        public void Task13_Setup()
        {
            BrowserSetup();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
        }

        private bool IsElementPresented(string xPath)
        {
            return driver.FindElements(By.XPath(xPath)).Count > 0;
        }
        private static void WaitTextPresented(IWebElement element, string text)
        {
            wait.Until(ExpectedConditions.TextToBePresentInElement(element, text));
        }
        [TestMethod]
        public void Task13_Case_CartOperations()
        {
            var counter = Convert.ToInt16(driver.FindElement(By.XPath(".//span[@class='quantity']")).Text);

            while (counter < 3)
            {
                driver.Navigate().GoToUrl(URL);
                driver.FindElement(By.XPath(".//div[contains(@id,'campaigns')]//li[contains(@class,'product')]"))
                    .Click();
                wait.Until(ExpectedConditions.ElementExists(By.XPath(".//h1[@class='title']")));
                if (IsElementPresented(".//select[@name='options[Size]']"))
                {
                    var selectElement = new SelectElement(driver.FindElement(By.XPath(".//select[@name='options[Size]']")));
                    selectElement.SelectByIndex(1);
                }

                driver.FindElement(By.XPath(".//button[@name='add_cart_product']")).Click();
                WaitTextPresented(driver.FindElement(By.XPath(".//span[@class='quantity']")), (counter + 1).ToString());

                counter += 1;
            }
            driver.FindElement(By.XPath(".//div[@id='cart-wrapper']//a[@class='link']")).Click();

            if (IsElementPresented(".//div[@id='order_confirmation-wrapper']"))
            {
                var tableElement = driver.FindElement(By.XPath(".//div[@id='order_confirmation-wrapper']"));
                var count = driver.FindElements(By.XPath(".//table[contains(@class,'data-table')]//tr[@class='item']"))
                                .Count - 1;

                do
                {
                    wait.Until((IWebDriver d) =>
                        d.FindElements(By.XPath(".//table[contains(@class,'data-table')]//tr[@class='item']"))
                            .Count -
                        1 == count);
                    driver.FindElement(By.XPath(".//button[@name='remove_cart_item']")).Click();
                    count -= 1;
                } while (count > 0);
                //Count !=0 doesn't work
                Assert.IsTrue(driver.FindElements(By.XPath(".//em[contains(text(),'There are no items')]")).Count > 0);
            }



        }

        [TestCleanup()]
        public void Task13_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}