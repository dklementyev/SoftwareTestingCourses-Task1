using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject.objects;
using TestProject.pages;

namespace TestProject.app
{
    public class Application
    {
        private IWebDriver driver;

        private CheckOutPage checkOutPage;
        private ProductListingPage productListingPage;
        private ProductDetailsPage productDetailsPage;

        private string siteUrl = "http://localhost/litecart";

        public Application()
        {
            driver = new ChromeDriver();
            checkOutPage = new CheckOutPage(driver);
            productListingPage = new ProductListingPage(driver);
            productDetailsPage = new ProductDetailsPage(driver);
        }
        #region siteOperations

        public void Quit()
        {
            driver.Quit();
        }

        public void OpenSite()
        {
            driver.Navigate().GoToUrl(siteUrl);
        }
        #endregion

        public void BuyProduct(Product product)
        {
            productListingPage.OpenProduct(product.Name);
            productDetailsPage.SelectSize(product.Size);
            productDetailsPage.AddToCart();
        }

        public void BuyProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                this.OpenSite();
                this.BuyProduct(product);
            }
        }

        public void DeleteAllProducts()
        {
            productListingPage.OpenCart();
            checkOutPage.DeleteAllItems();
        }
    }
    
}
