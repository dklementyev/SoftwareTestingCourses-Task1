using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Task1
{
    [TestClass]
    public class Task2Test1
    {
        #region Constants
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string ADMIN_URL = "localhost:8081/admin";
        private const int WAIT_TIMEOUT = 10;
        #endregion

        private IWebDriver driver;
        private WebDriverWait wait;

        [TestInitialize()]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(ADMIN_URL);
        }

        [TestMethod()]
        public void Task2_1TestCase()
        {
            wait.Until(ExpectedConditions.TitleIs("My Store"));
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
        public void TestTeardown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
