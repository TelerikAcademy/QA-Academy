namespace QAAcademyDemo.Telerik.Core.Pages_02.BillingInformationPage
{
    using OpenQA.Selenium;
using System;
using System.Collections.Generic;
    using System.Linq;
    using QAAcademyDemo.Telerik.Core.Pages_02;

    public partial class BillingInformationPage : BasePage
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
            this.BillingFirstName.SendKeys(firstName);
            this.BillingLastName.SendKeys(lastName);
            this.BillingCity.SendKeys(city);
        }

        public void FillShippingInformation(string firstName, string lastName, string city)
        {
            this.ShippingFirstName.SendKeys(firstName);
            this.ShippingLastName.SendKeys(lastName);
            this.ShippingCity.SendKeys(city);
        }
    }
}
