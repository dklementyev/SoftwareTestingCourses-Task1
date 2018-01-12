using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace TestProject.pages
{
    public class CheckOutPage : BasePage
    {
        public CheckOutPage(IWebDriver driver) : base(driver)
        {

        }

        public void DeleteAllItems()
        {
            if (IsElementPresented(".//div[@id='order_confirmation-wrapper']"))
            {
                var tableElement = driver.FindElement(By.XPath(".//div[@id='order_confirmation-wrapper']"));
                var count = driver.FindElements(By.XPath(".//table[contains(@class,'data-table')]//tr[@class='item']"))
                                .Count - 1;

                do
                {
                    wait.Until((IWebDriver d) =>
                        d.FindElements(By.XPath(".//table[contains(@class,'data-table')]//tr[@class='item']"))
                            .Count -
                        1 == count);
                    driver.FindElement(By.XPath(".//button[@name='remove_cart_item']")).Click();
                    count -= 1;
                } while (count > 0);

                Assert.IsTrue(IsElementPresented(".//em[contains(text(),'There are no items')]"));
                //Count !=0 doesn't work
                //Assert.IsTrue(driver.FindElements(By.XPath(".//em[contains(text(),'There are no items')]")).Count > 0);

            }
        }
    }
}
