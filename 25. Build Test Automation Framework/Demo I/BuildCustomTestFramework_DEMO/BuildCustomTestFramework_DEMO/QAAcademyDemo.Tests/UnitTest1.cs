using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using QAAcademyDemo.Telerik.Core.Facades;
using QAAcademyDemo.Telerik.Core.Model;
using QAAcademyDemo.Telerik.Core.Pages_02.BillingInformationPage;
using QAAcademyDemo.Telerik.Core.Pages_02.ShoppingCartPage;

namespace QAAcademyDemo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreatePurchaseBulgaria_AndValidateNoVATTax()
        {
            IWebDriver driver = new FirefoxDriver();

            ShoppingCartPage shoppingCartPage = new ShoppingCartPage(driver);
            BillingInformationPage billingInformationPage = new BillingInformationPage(driver);
            OnlineShoppingCartFacade onlineShoppingCart = new OnlineShoppingCartFacade(shoppingCartPage, billingInformationPage);
            BillingInformation billingInfo = new BillingInformation("Anton", "Angelov", "Kaspichan");
            ShippingInformation shippingInfo = new ShippingInformation("Anton", "Angelov", "Kalofer");

            onlineShoppingCart.PerformPurchase(billingInfo, shippingInfo);
        }

        [TestMethod]
        public void FluetPageTest()
        {
            IWebDriver driver = new FirefoxDriver();

            var shoppingCartPage = 
                new QAAcademyDemo.Telerik.Core.Pages_01.ShoppingCartPage.ShoppingCartPage(driver);
            ////shoppingCartPage.ChangeQuantity(1).SetCoupon("").Asserter.AssertCheckoutButtonIsEnabled(...);
        }
    }
}
