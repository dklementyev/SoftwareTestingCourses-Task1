using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task1
{
    [TestClass]
    public class Task10
    {
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string ADMIN_URL = "http://localhost/litecart";
        private const int WAIT_TIMEOUT = 10;
        private const string EXPECTED_TITLE = "Online Store | My Store";

        private IWebDriver driver;
        private WebDriverWait wait;

        [TestInitialize()]
        public void Task10_Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(ADMIN_URL);
            wait.Until(ExpectedConditions.TitleIs(EXPECTED_TITLE));
        }

        private List<string> ConvertColorToList(string color)
        {
            var correctRgba = color.Replace("rgba(", "").Replace(")", "").Replace(" ", "");
            var rgbaList = correctRgba.Split(',').ToList();

            return rgbaList;
        }
        private bool IsCostGray(IWebElement costElement)
        {
            var rgbaColor = costElement.GetCssValue("color");
            var rgbaList = ConvertColorToList(rgbaColor);
            if (rgbaList[0].Equals(rgbaList[1]) && rgbaList[1].Equals(rgbaList[2]))
            {
                return true;
            }
            return false;
        }

        private bool IsCostRed(IWebElement costElement)
        {
            var rgbaColor = costElement.GetCssValue("color");
            var rgbaList = ConvertColorToList(rgbaColor);
            if (rgbaList[1].Equals("0") && rgbaList[2].Equals("0"))
            {
                return true;
            }
            return false;
        }
        [TestMethod]
        public void Task10_CheckCorrectPage()
        {
            ////Login

            //var loginField = this.driver.FindElement(By.XPath(".//input[@name='username']"));
            //var pwdField = this.driver.FindElement(By.XPath(".//input[@name='password']"));
            //var loginBtn = this.driver.FindElement(By.XPath(".//button[@name='login']"));

            //loginField.Clear();
            //loginField.SendKeys(ADMIN_LOGIN);

            //pwdField.Clear();
            //pwdField.SendKeys(ADMIN_PASSWORD);

            //loginBtn.Click();
            //wait.Until(ExpectedConditions.ElementExists(By.XPath(".//img[@title='My Store']")));

            //Product Listing
            var product = driver.FindElement(By.XPath(".//div[contains(@id,'campaigns')]//li[contains(@class,'product')]"));

            var productNameLabel = product.FindElement(By.XPath(".//div[@class='name']"));
            var costLabel = product.FindElement(By.XPath(".//div[contains(@class,'price')]//s[contains(@class,'regular')]"));
            var discountCostLabel = product.FindElement(By.XPath(".//div[contains(@class,'price')]//strong[contains(@class,'campaign')]")); ;

            var productName = productNameLabel.Text;
            var cost = costLabel.Text;
            var discountCost = discountCostLabel.Text;

            Assert.IsTrue(IsCostGray(costLabel));
            Assert.IsTrue(IsCostRed(discountCostLabel));
            var digCost = Convert.ToInt16(cost.Replace("$",""));
            var digDiscountCost = Convert.ToInt16(discountCost.Replace("$", ""));
            Assert.IsTrue(digCost > digDiscountCost);
            product.Click();

            //Product Details

            productNameLabel = driver.FindElement(By.XPath(".//h1[@class='title']"));
            costLabel = driver.FindElement(By.XPath(".//s[@class='regular-price']"));
            discountCostLabel = driver.FindElement(By.XPath(".//strong[@class='campaign-price']"));

            Assert.IsTrue(productNameLabel.Text.Equals(productName));
            Assert.IsTrue(costLabel.Text.Equals(cost));
            Assert.IsTrue(discountCostLabel.Text.Equals(discountCost));

            digCost = Convert.ToInt16(costLabel.Text.Replace("$", ""));
            digDiscountCost = Convert.ToInt16(discountCostLabel.Text.Replace("$", ""));
            Assert.IsTrue(digCost > digDiscountCost);

            Assert.IsTrue(IsCostGray(costLabel));
            Assert.IsTrue(IsCostRed(discountCostLabel));
        }

        [TestCleanup()]
        public void Task10_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
