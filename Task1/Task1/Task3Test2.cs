using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace Task1
{
    [TestClass]
    public class Task3Test2
    {
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string ADMIN_URL = "http://localhost:8081/admin";
        private const int WAIT_TIMEOUT = 10;
        private const string EXPECTED_TITLE = "My Store";

        private IWebDriver driver;
        private WebDriverWait wait;
        //FireFoxNew
        [TestInitialize()]
        public void Task3_Setup()
        {
            driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(ADMIN_URL);
        }
        [TestMethod()]
        public void Task3Test1_Case()
        {
            wait.Until(ExpectedConditions.TitleIs(EXPECTED_TITLE));
            var loginField = this.driver.FindElement(By.XPath(".//input[@name='username']"));
            var pwdField = this.driver.FindElement(By.XPath(".//input[@name='password']"));
            var loginBtn = this.driver.FindElement(By.XPath(".//button[@name='login']"));

            loginField.Clear();
            loginField.SendKeys(ADMIN_LOGIN);

            pwdField.Clear();
            pwdField.SendKeys(ADMIN_PASSWORD);

            loginBtn.Click();
        }
        [TestCleanup()]
        public void Task3_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
