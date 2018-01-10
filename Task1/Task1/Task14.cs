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
    public class Task14
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
        public void Task14_Setup()
        {
            BrowserSetup();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
        }

        [TestMethod]
        public void Task14_Case_Windows()
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

            driver.FindElement(By.XPath(".//li[contains(@id,'app-')]/a//span[contains(text(),'Countries')]")).Click();
            driver.FindElement(By.XPath(".//a[@title='Edit']")).Click();

            var mWin = driver.CurrentWindowHandle;
            var extLinks = driver.FindElements(By.XPath(".//i[contains(@class,'external-link')]"));

            foreach (var elem in extLinks)
            {
                elem.Click();

                wait.Until(
                    (dr) =>
                    {
                        var wins = dr.WindowHandles;
                        foreach (var w in wins)
                        {
                            if (!w.Equals(mWin))
                            {
                                return dr.SwitchTo().Window(w);
                            }
                        }
                        return null;
                    });

                driver.Close();
                driver.SwitchTo().Window(mWin);
            }
        }

        [TestCleanup()]
        public void Task14_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}