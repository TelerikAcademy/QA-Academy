namespace QAAcademyDemo.Telerik.Core.Pages_02.ShoppingCartPage
{
    using Base02;

    using OpenQA.Selenium;

    public partial class ShoppingCartPage : BasePage
    {
        public ShoppingCartPage(IWebDriver driver) : base(driver)
        {
        }

        public void ChangeQuantity(int quantity)
        {
            ////this.Map.Quantity
        }

        public override void Navigate()
        {
            this.Driver.Navigate().GoToUrl("https://www.telerik.com/account/shopping-cart.aspx");
        }
    }
}