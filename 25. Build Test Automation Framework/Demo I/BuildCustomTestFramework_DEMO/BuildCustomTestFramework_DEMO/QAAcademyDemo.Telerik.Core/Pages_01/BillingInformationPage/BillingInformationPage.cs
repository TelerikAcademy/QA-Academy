namespace QAAcademyDemo.Telerik.Core.Pages_01.BillingInformationPage
{
    using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;


    public class BillingInformationPage : BasePage<BillingInformationPageMap, BillingInformationPageAsserter>
    {
        public BillingInformationPage(IWebDriver driver)
            : base(driver)
        {
        }

        public override void Navigate()
        {
            throw new NotImplementedException();
        }

        public void FillBillingInformation(string firstName, string lastName, string city)
        {
            this.Map.BillingFirstName.SendKeys(firstName);
            this.Map.BillingLastName.SendKeys(lastName);
            this.Map.BillingCity.SendKeys(city);
        }

        public void FillShippingInformation(string firstName, string lastName, string city)
        {
            this.Map.ShippingFirstName.SendKeys(firstName);
            this.Map.ShippingLastName.SendKeys(lastName);
            this.Map.ShippingCity.SendKeys(city);
        }
    }
}
