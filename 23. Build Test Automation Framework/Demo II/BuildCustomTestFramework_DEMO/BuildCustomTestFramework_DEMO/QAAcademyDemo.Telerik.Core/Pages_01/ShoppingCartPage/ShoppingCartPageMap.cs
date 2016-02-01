namespace QAAcademyDemo.Telerik.Core.Pages_01.ShoppingCartPage
{
    using System;

    using OpenQA.Selenium;

    public class ShoppingCartPageMap : BaseElementMap
    {
        public ShoppingCartPageMap(IWebDriver driver)
            : base(driver)
        {
        }

        public ShoppingCartPageMap()
        {
            throw new NotImplementedException();
        }

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