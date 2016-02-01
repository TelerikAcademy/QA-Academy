namespace QAAcademyDemo.Telerik.Core.Pages_02.ShoppingCartPage
{
    using OpenQA.Selenium;

    public partial class ShoppingCartPage
    {
        public IWebElement Quantity
        {
            get
            {
                return
                    this.Driver.FindElement(
                        By.XPath(
                            "//*[@id='ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_clientorder_shoppingcart_ascx1_radGridProducts_ctl00_ctl04_ddlQuantity']"));
            }
        }

        public IWebElement Checkout
        {
            get
            {
                return
                    this.Driver.FindElement(
                        By.XPath(
                            "//*[@id='ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_clientorder_shoppingcart_ascx1_radGridProducts_ctl00_ctl04_ddlQuantity']"));
            }
        }
    }
}