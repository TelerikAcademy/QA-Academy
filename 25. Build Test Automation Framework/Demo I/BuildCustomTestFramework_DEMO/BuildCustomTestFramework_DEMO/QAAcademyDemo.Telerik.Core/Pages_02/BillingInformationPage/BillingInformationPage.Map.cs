namespace QAAcademyDemo.Telerik.Core.Pages_02.BillingInformationPage
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public partial class BillingInformationPage
    {
        internal IWebElement BillingFirstName
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//*[@id='ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_clientorder_shoppingcart_ascx1_radGridProducts_ctl00_ctl04_ddlQuantity']"));
            }
        }

        public IWebElement BillingLastName
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//*[@id='ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_clientorder_shoppingcart_ascx1_radGridProducts_ctl00_ctl04_ddlQuantity']"));
            }
        }

        public IWebElement BillingCity
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//*[@id='ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_clientorder_shoppingcart_ascx1_radGridProducts_ctl00_ctl04_ddlQuantity']"));
            }
        }

        public IWebElement ShippingFirstName
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//*[@id='ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_clientorder_shoppingcart_ascx1_radGridProducts_ctl00_ctl04_ddlQuantity']"));
            }
        }

        public IWebElement ShippingLastName
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//*[@id='ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_clientorder_shoppingcart_ascx1_radGridProducts_ctl00_ctl04_ddlQuantity']"));
            }
        }

        public IWebElement ShippingCity
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//*[@id='ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_clientorder_shoppingcart_ascx1_radGridProducts_ctl00_ctl04_ddlQuantity']"));
            }
        }

        public IWebElement ProceedToPayment
        {
            get
            {
                return this.Driver.FindElement(By.XPath("//*[@id='ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_clientorder_shoppingcart_ascx1_radGridProducts_ctl00_ctl04_ddlQuantity']"));
            }
        }
    }
}
