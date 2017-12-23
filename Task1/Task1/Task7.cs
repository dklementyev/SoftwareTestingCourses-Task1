using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Chrome;

namespace Task1
{
    [TestClass]
    public class Task7
    {
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string ADMIN_URL = "http://localhost/litecart/admin";
        private const int WAIT_TIMEOUT = 10;
        private const string EXPECTED_TITLE = "My Store";

        private IWebDriver driver;
        private WebDriverWait wait;

        [TestInitialize()]
        public void Task7_Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(ADMIN_URL);
            wait.Until(ExpectedConditions.TitleIs(EXPECTED_TITLE));
        }
        [TestMethod()]
        public void Task7_Case()
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
            //ClickOnAllMenuItems
            
            ReadOnlyCollection<IWebElement> menus = driver.FindElements(By.XPath(".//li[contains(@id,'app-')]/a"));
            ReadOnlyCollection<IWebElement> submenus;

            for (int i = 0; i < menus.Count; ++i)
            {
                menus = driver.FindElements(By.XPath(".//li[contains(@id,'app-')]/a"));
                menus[i].Click();
                wait.Until(ExpectedConditions.ElementExists(By.XPath(".//h1")));
                submenus = driver.FindElements(By.XPath(".//li[contains(@id,'doc-')]/a"));
                for(int j = 0; j < submenus.Count; ++j)
                {
                    submenus = driver.FindElements(By.XPath(".//li[contains(@id,'doc-')]/a"));
                    submenus[j].Click();
                    wait.Until(ExpectedConditions.ElementExists(By.XPath(".//h1")));
                }
            }
        }
        [TestCleanup()]
        public void Task7_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
