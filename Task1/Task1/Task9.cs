using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task1
{
    [TestClass]
    public class Task9
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
        public void Task9_CheckSort()
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
            //Click On Countries Menu
            driver.FindElement(By.XPath(".//li[contains(@id,'app-')]/a//span[contains(text(),'Countries')]")).Click();
            wait.Until(ExpectedConditions.TitleIs("Countries | My Store"));
            //Find all countries elements and put it into list
            var rows = driver.FindElements(By.XPath(".//table//tr[@class='row']"));
            var inputNames = new List<string>();
            var sortedNames = new List<string>();
            var countOfCountries = rows.Count; 
            for(var i = 0; i<countOfCountries; ++i)
            {
                rows = driver.FindElements(By.XPath(".//table//tr[@class='row']"));
                var country =
                    rows[i].FindElement(By.XPath(".//a[contains(@href,'countries') and not(contains(@title,'Edit'))]"));
                var countOfSubZones = rows[i].FindElement(By.XPath(".//td[6]"));
                sortedNames.Add(country.Text);
                if (!countOfSubZones.Text.Equals("0"))
                {
                    country.Click();
                    wait.Until(ExpectedConditions.TitleIs("Edit Country | My Store"));
                    var inputSubNames = new List<string>();
                    var sortedSubNames = new List<string>();
                    var subRows = driver.FindElements(By.XPath(".//table//row[not(class='header')]"));
                    foreach (var subRow in subRows)
                    {
                        sortedSubNames.Add(subRow.GetAttribute("value"));
                    }

                    inputSubNames = sortedSubNames;
                    sortedSubNames.Sort();
                    Assert.AreEqual(inputSubNames,sortedSubNames);
                    var cancelButton = driver.FindElement(By.XPath(".//button[contains(@name,'cancel')]"));
                    cancelButton.Click();
                }
            }

            inputNames = sortedNames;
            sortedNames.Sort();
            Assert.AreEqual(inputNames, sortedNames);
        }

        [TestCleanup()]
        public void Task9_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
