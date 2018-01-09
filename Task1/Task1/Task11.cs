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
    public class Task11
    {
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string URL = "http://localhost/litecart";
        private const int WAIT_TIMEOUT = 10;
        private const string EXPECTED_TITLE = "Online Store | My Store";
        private const string BROWSER = "Chrome";
        private IWebDriver driver;
        private WebDriverWait wait;



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
        public void Task11_Setup()
        {
            BrowserSetup();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
            wait.Until(ExpectedConditions.TitleIs(EXPECTED_TITLE));
        }

        [TestMethod]
        public void Task11_Case_CreateCustomer()
        {
            //Click on New Customer Link
            driver.FindElement(By.XPath(".//a[contains(text(),'New customers')]")).Click();

            //Fill new customer fields
            var firstNameField = driver.FindElement(By.XPath(".//input[@name='firstname']"));
            var lastNameField = driver.FindElement(By.XPath(".//input[@name='lastname']"));
            var address1Field = driver.FindElement(By.XPath(".//input[@name='address1']"));
            var postCodeField = driver.FindElement(By.XPath(".//input[@name='postcode']"));
            var cityField = driver.FindElement(By.XPath(".//input[@name='city']"));
            var countrySelectElement = driver.FindElement(By.XPath(".//select[@name='country_code']"));
            var emailField = driver.FindElement(By.XPath(".//input[@name='email']"));
            var phoneField = driver.FindElement(By.XPath(".//input[@name='phone']"));
            var passwordField = driver.FindElement(By.XPath(".//input[@name='password']"));
            var confirmPasswordField = driver.FindElement(By.XPath(".//input[@name='confirmed_password']"));
            var createAccountButton = driver.FindElement(By.XPath(".//button[@name='create_account']"));

            firstNameField.Clear();
            firstNameField.SendKeys("Test FName");

            lastNameField.Clear();
            lastNameField.SendKeys("Test LName");

            address1Field.Clear();
            address1Field.SendKeys("Test Address1");

            postCodeField.Clear();
            postCodeField.SendKeys("55555");

            cityField.Clear();
            cityField.SendKeys("Test City");

            var countrySelector = new SelectElement(countrySelectElement);
            countrySelector.SelectByValue("US");

            var email = String.Format("TestEmail{0}@email.com", Guid.NewGuid());
            emailField.Clear();
            emailField.SendKeys(email);

            phoneField.SendKeys("+15555555555");

            var password = "TestPassword";
            passwordField.Clear();
            passwordField.SendKeys(password);

            confirmPasswordField.Clear();
            confirmPasswordField.SendKeys(password);


            //Click on create account button
            createAccountButton.Click();

             
            //Logout
            var logoutLink = driver.FindElement(By.XPath(".//a[text()='Logout']"));
            logoutLink.Click();

            //LogIn
            var loginField = driver.FindElement(By.XPath(".//input[@name='email']"));
            var pwdField = driver.FindElement(By.XPath(".//input[@name='password']"));
            var loginButton = driver.FindElement(By.XPath(".//button[@name='login']"));

            loginField.Clear();
            loginField.SendKeys(email);

            pwdField.Clear();
            pwdField.SendKeys(password);

            loginButton.Click();

            //Logout again
            logoutLink = driver.FindElement(By.XPath(".//a[text()='Logout']"));
            logoutLink.Click();

        }

        [TestCleanup()]
        public void Task11_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
