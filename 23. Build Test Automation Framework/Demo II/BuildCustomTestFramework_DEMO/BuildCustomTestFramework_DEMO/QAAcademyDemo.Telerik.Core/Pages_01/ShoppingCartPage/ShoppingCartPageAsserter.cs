namespace QAAcademyDemo.Telerik.Core.Pages_01.ShoppingCartPage
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;

    public class ShoppingCartPageAsserter
    {
        public void AssertCheckoutButtonIsEnabled(IWebElement checkoutButton)
        {
            Assert.IsTrue(checkoutButton.Enabled, "The checkout button was not enabled.");
        }

        public void AssertCheckoutButtonIsEnabled(bool observedCheckoutEnabledValue)
        {
            Assert.IsTrue(observedCheckoutEnabledValue, "The checkout button was not enabled.");
        }
    }
}