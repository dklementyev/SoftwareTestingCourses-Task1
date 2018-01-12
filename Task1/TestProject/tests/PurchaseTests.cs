using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.objects;

namespace TestProject.tests
{
    [TestClass]
    public class PurchaseTests : TestBase
    {
        [TestMethod]
        public void Case_CheckCartWork()
        {

            #region GenerateTestData

            List<Product> products = new List<Product>();

            for (int i = 0; i < 3; ++i)
            {
                products.Add(new Product());
            }

            #endregion

            app.OpenSite();
            app.BuyProducts(products);
            app.DeleteAllProducts();

        }
    }
}
