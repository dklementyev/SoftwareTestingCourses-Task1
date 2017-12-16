using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class CommonTestSteps
    {
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string ADMIN_URL = "http://localhost:8081/admin";
        private const int WAIT_TIMEOUT = 10;
        private const string ADMIN_PORTAL_TITLE = "My Store";
        private const string BROWSER = "Chrome";

        public IWebDriver driver;
        public WebDriverWait wait;
        

        public CommonTestSteps()
        {
            switch (BROWSER.ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver();
                    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
                    driver.Manage().Window.Maximize();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
                    driver.Manage().Window.Maximize();
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
                    driver.Manage().Window.Maximize();
                    break;
            }
        }

        public void OpenAdminPortal()
        {
            driver.Navigate().GoToUrl(ADMIN_URL);
            wait.Until(ExpectedConditions.TitleIs(ADMIN_PORTAL_TITLE));
        }
    }
}
