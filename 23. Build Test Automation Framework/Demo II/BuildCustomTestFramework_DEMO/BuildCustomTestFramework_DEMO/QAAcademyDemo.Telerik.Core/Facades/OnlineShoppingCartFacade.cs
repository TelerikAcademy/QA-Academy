namespace QAAcademyDemo.Telerik.Core.Facades
{
    using Model;

    using Pages_02.BillingInformationPage;
    using Pages_02.ShoppingCartPage;

    public class OnlineShoppingCartFacade
    {
        public OnlineShoppingCartFacade(
            BillingInformationPage billingInformationPage,
            ShoppingCartPage shoppingCartPage)
        {
            this.BillingInformationPage = billingInformationPage;
            this.ShoppingCartPage = shoppingCartPage;
        }

        public BillingInformationPage BillingInformationPage { get; set; }

        public ShoppingCartPage ShoppingCartPage { get; set; }

        public void PerformPurchase(BillingInformation billingInfo, ShippingInformation shippingInfo)
        {
            ////shoppingCartPage.ChangeQuantity(1);
            this.ShoppingCartPage.Checkout.Click();
            this.ShoppingCartPage.AssertCheckoutButtonIsEnabled();
            this.ShoppingCartPage.AssertCheckoutButtonIsEnabled();

            this.BillingInformationPage.AssertCityLabel();
            this.BillingInformationPage.AssertFirstNameLabel();
            this.BillingInformationPage.AssertLastNameLabel();
            this.BillingInformationPage.FillBillingInformation(billingInfo.FirstName,
                billingInfo.LastName,
                billingInfo.City);
            this.BillingInformationPage.FillShippingInformation(shippingInfo.FirstName,
                shippingInfo.LastName,
                shippingInfo.City);
            this.BillingInformationPage.ProceedToPayment.Click();
        }
    }
}