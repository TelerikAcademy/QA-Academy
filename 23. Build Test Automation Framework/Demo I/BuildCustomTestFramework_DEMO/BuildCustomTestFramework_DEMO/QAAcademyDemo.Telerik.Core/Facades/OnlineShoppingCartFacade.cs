namespace QAAcademyDemo.Telerik.Core.Facades
{
    using OpenQA.Selenium;
    using QAAcademyDemo.Telerik.Core.Model;
    using QAAcademyDemo.Telerik.Core.Pages_01.BillingInformationPage;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using QAAcademyDemo.Telerik.Core.Pages_02.ShoppingCartPage;

    public class OnlineShoppingCartFacade
    {
        private readonly BillingInformationPage billingInformationPage;
        private readonly ShoppingCartPage shoppingCartPage;

        public OnlineShoppingCartFacade(
            BillingInformationPage billingInformationPage,
            ShoppingCartPage shoppingCartPage)
        {
            this.billingInformationPage = billingInformationPage;
            this.shoppingCartPage = shoppingCartPage;
        }

        public BillingInformationPage BillingInformationPage 
        {
            get
            {
                return this.billingInformationPage;
            }
        }

        public ShoppingCartPage ShoppingCartPage
        {
            get
            {
                return this.shoppingCartPage;
            }
        }

        public void PerformPurchase(BillingInformation billingInfo, ShippingInformation shippingInfo)
        {
            ////shoppingCartPage.ChangeQuantity(1);
            shoppingCartPage.Checkout.Click();
            shoppingCartPage.AssertCheckoutButtonIsEnabled();
            shoppingCartPage.AssertCheckoutButtonIsEnabled();

            billingInformationPage.Asserter.AssertCityLabel();
            billingInformationPage.Asserter.AssertFirstNameLabel();
            billingInformationPage.Asserter.AssertLastNameLabel();
            billingInformationPage.FillBillingInformation(billingInfo.FirstName, billingInfo.LastName, billingInfo.City);
            billingInformationPage.FillShippingInformation(shippingInfo.FirstName, shippingInfo.LastName, shippingInfo.City);
            billingInformationPage.Map.ProceedToPayment.Click();
        }
    }
}
