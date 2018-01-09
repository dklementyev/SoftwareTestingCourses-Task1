using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Task1
{
    [TestClass]
    public class Task12
    {
        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string URL = "http://localhost/litecart/admin";
        private const int WAIT_TIMEOUT = 10;
        private const string EXPECTED_TITLE = "My Store";
        private const string BROWSER = "Chrome";
        private IWebDriver driver;
        private WebDriverWait wait;
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
        public void Task12_Setup()
        {
            BrowserSetup();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIMEOUT));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
            wait.Until(ExpectedConditions.TitleIs(EXPECTED_TITLE));
        }

        [TestMethod]
        public void Task12_Case_CreateCustomer()
        {
            //Login

            wait.Until(ExpectedConditions.TitleIs(EXPECTED_TITLE));
            var loginField = this.driver.FindElement(By.XPath(".//input[@name='username']"));
            var pwdField = this.driver.FindElement(By.XPath(".//input[@name='password']"));
            var loginBtn = this.driver.FindElement(By.XPath(".//button[@name='login']"));

            loginField.Clear();
            loginField.SendKeys(ADMIN_LOGIN);

            pwdField.Clear();
            pwdField.SendKeys(ADMIN_PASSWORD);

            loginBtn.Click();

            driver.FindElement(By.XPath(".//li[contains(@id,'app-')]/a//span[contains(text(),'Catalog')]")).Click();

            driver.FindElement(By.XPath(".//a[contains(text(),'Add New Product')]")).Click();

            driver.FindElement(By.XPath(".//input[@name='status' and @value='1']")).Click();

            var nameField = driver.FindElement(By.XPath(".//input[contains(@name,'name')]"));
            var codeField = driver.FindElement(By.XPath(".//input[contains(@name,'code')]"));
            driver.FindElement(By.XPath($".//td[text()='{PRODUCT_GROUP}']/../td[1]")).Click();
            var quantityField = driver.FindElement(By.XPath(".//input[@name='quantity']"));
            var quantityUnitSelect = driver.FindElement(By.XPath(".//select[@name='quantity_unit_id']"));
            var deliveryStatusSelect = driver.FindElement(By.XPath(".//select[@name='delivery_status_id']"));
            var soldOutStatusSelect = driver.FindElement(By.XPath(".//select[@name='sold_out_status_id']"));
            var dateValidFrom = driver.FindElement(By.XPath(".//input[@name='date_valid_from']"));
            var dateValidTo = driver.FindElement(By.XPath(".//input[@name='date_valid_to']"));

            var imagePath = Directory.GetCurrentDirectory() + @"\images\productImage.jpg";
            var productImageUpload = driver.FindElement(By.XPath(".//input[@name='new_images[]']"));


            var productName = $"Test Product {Guid.NewGuid()}";
            nameField.Clear();
            nameField.SendKeys(productName);

            var productCode = $"Test Code {Guid.NewGuid()}";
            codeField.Clear();
            codeField.SendKeys(productCode);

            quantityField.Clear();
            quantityField.SendKeys("1");

            var quantityUnitSelector = new SelectElement(quantityUnitSelect);
            quantityUnitSelector.SelectByText("pcs");

            var deliveryStatusSelector = new SelectElement(deliveryStatusSelect);
            deliveryStatusSelector.SelectByText("3-5 days");

            var soldOutStatusSelector = new SelectElement(soldOutStatusSelect);
            soldOutStatusSelector.SelectByText("Sold out");

            productImageUpload.SendKeys(imagePath);

            dateValidFrom.SendKeys("01.01.1970");

            dateValidTo.SendKeys("31.12.2018");

            var informationTab = driver.FindElement(By.XPath(".//a[text()='Information']"));

            informationTab.Click();

            System.Threading.Thread.Sleep(5000);

            var manufacturerSelect = driver.FindElement(By.XPath(".//select[@name='manufacturer_id']"));
            //var supplierSelect = driver.FindElement(By.XPath(".//select[@name='supplier_id']"));
            var keywordsField = driver.FindElement(By.XPath(".//input[@name='keywords']"));
            var shortDescription = driver.FindElement(By.XPath(".//input[contains(@name,'short_description')]"));
            var descriptionField = driver.FindElement(By.XPath(".//div[contains(@class,'trumbowyg-editor')]"));
            var headTitleField = driver.FindElement(By.XPath(".//input[contains(@name,'head_title')]"));
            var metaDescriptionField = driver.FindElement(By.XPath(".//input[contains(@name,'meta_description')]"));
            
            var manufacturerSelector = new SelectElement(manufacturerSelect);
            manufacturerSelector.SelectByText("ACME Corp.");

            keywordsField.Clear();
            keywordsField.SendKeys("KeyWords");
            
            shortDescription.Clear();
            shortDescription.SendKeys("Short Description");

            new Actions(driver).SendKeys(descriptionField,"Description").Perform();

            headTitleField.Clear();
            headTitleField.SendKeys("HeadTitle");

            metaDescriptionField.Clear();
            metaDescriptionField.SendKeys("Meta Description");


            var pricesTab = driver.FindElement(By.XPath(".//a[text()='Prices']"));
            pricesTab.Click();

            var purchasePriceField = driver.FindElement(By.XPath(".//input[@name='purchase_price']"));
            var currencySelect = driver.FindElement(By.XPath(".//select[@name='purchase_price_currency_code']"));
            var saveButton = driver.FindElement(By.XPath(".//button[@name='save']"));

            purchasePriceField.Clear();
            purchasePriceField.SendKeys("1,00");
            var currencySelector = new SelectElement(currencySelect);
            currencySelector.SelectByValue("USD");

            saveButton.Click();

         }

        [TestCleanup()]
        public void Task12_TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
