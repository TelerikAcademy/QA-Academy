namespace QAAcademyDemo.Telerik.Core.Pages_02.BillingInformationPage
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class BillingInformationPageAsserter
    {
        public static void AssertFirstNameLabel(this BillingInformationPage page)
        {
            Assert.IsTrue(page.BillingFirstName.Enabled);
        }

        public static void AssertLastNameLabel(this BillingInformationPage page)
        {
        }

        public static void AssertCityLabel(this BillingInformationPage page)
        {
        }
    }
}