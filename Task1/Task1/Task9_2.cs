using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task1
{
    [TestClass]
    public class Task9_2
    {
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string ADMIN_URL = "http://localhost/litecart/admin";
        private const int WAIT_TIMEOUT = 10;
        private const string EXPECTED_TITLE = "My Store";

        private IWebDriver driver;
        private WebDriverWait wait;

        [TestInitialize()]
        public void Task9_Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(ADMIN_URL);
            wait.Until(ExpectedConditions.TitleIs(EXPECTED_TITLE));
        }

        [TestMethod]
        public void Task9_2_CheckSort()
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
            wait.Until(ExpectedConditions.ElementExists(By.XPath(".//img[@title='My Store']")));
            //Click On Geo Zones Menu
            driver.FindElement(By.XPath(".//li[contains(@id,'app-')]/a//span[contains(text(),'Geo Zones')]")).Click();

            //Find all geo zones

            var zones = driver.FindElements(By.XPath(".//tr[@class='row']/td[3]"));
            for (var i = 0; i < zones.Count; i++)
            {
                zones = driver.FindElements(By.XPath(".//tr[@class='row']/td[3]/a"));
                //withou it trick doesn't work...
                System.Threading.Thread.Sleep(1000);
                zones[i].Click();


                var subZones = driver.FindElements(By.XPath(".//tr[not(@class='header')]"));
                var listSubZones = new List<string>();
                for (var j = 0; j < subZones.Count; ++j)
                {
                    subZones = driver.FindElements(By.XPath(".//tr[not(@class='header')]"));
                    listSubZones.Add(subZones[i].Text);
                }

                var inputList = listSubZones;
                listSubZones.Sort();

                Assert.AreEqual(inputList,listSubZones);
                var cancelButton = driver.FindElement(By.XPath(".//button[contains(@name,'cancel')]"));
                cancelButton.Click();
            }

        }

        [TestCleanup()]
        public void Task9_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
