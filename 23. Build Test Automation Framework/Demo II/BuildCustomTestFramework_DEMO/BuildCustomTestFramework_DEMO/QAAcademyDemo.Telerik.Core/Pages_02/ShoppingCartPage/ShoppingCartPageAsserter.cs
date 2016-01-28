namespace QAAcademyDemo.Telerik.Core.Pages_02.ShoppingCartPage
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class ShoppingCartPageAsserter
    {
        public static void AssertCheckoutButtonIsEnabled(this ShoppingCartPage page)
        {
            Assert.IsTrue(page.Checkout.Enabled, "The checkout button was not enabled.");
        }
    }
}