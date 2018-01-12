﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProject.pages
{
    public class ProductListingPage : BasePage
    {
        public ProductListingPage(IWebDriver driver) : base(driver)
        {
        }

        internal void OpenProduct(string name)
        {
            driver.FindElement(By.XPath(".//div[contains(@id,'campaigns')]//li[contains(@class,'product')]"))
                .Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath(".//h1[@class='title']")));

        }

        public void OpenCart()
        {
            driver.FindElement(By.XPath(".//div[@id='cart-wrapper']//a[@class='link']")).Click();
        }

        
    }
}
