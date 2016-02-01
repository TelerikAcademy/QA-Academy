namespace QAAcademyDemo.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    using Telerik.Core.Facades;
    using Telerik.Core.Model;
    using Telerik.Core.Pages_02.BillingInformationPage;
    using Telerik.Core.Pages_02.ShoppingCartPage;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreatePurchaseBulgaria_AndValidateNoVATTax()
        {
            IWebDriver driver = new FirefoxDriver();

            var shoppingCartPage = new ShoppingCartPage(driver);
            var billingInformationPage = new BillingInformationPage(driver);
            var onlineShoppingCart = new OnlineShoppingCartFacade(billingInformationPage, shoppingCartPage);
            var billingInfo = new BillingInformation("Anton", "Angelov", "Kaspichan");
            var shippingInfo = new ShippingInformation("Anton", "Angelov", "Kalofer");

            onlineShoppingCart.PerformPurchase(billingInfo, shippingInfo);
        }

        [TestMethod]
        public void FluetPageTest()
        {
            IWebDriver driver = new FirefoxDriver();

            var shoppingCartPage =
                new Telerik.Core.Pages_01.ShoppingCartPage.ShoppingCartPage(driver);
            ////shoppingCartPage.ChangeQuantity(1).SetCoupon("").Asserter.AssertCheckoutButtonIsEnabled(...);
        }
    }
}