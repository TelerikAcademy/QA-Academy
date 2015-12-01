using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TelerikAcademyWebDriver.Advanced.Telerik.Pages
{
    public class YourAccountMainPage
    {
        [FindsBy(How = How.CssSelector, Using = "span.telerik-hi-user-name.js-telerik-user-first-name")]
        public IWebElement YourAccountNameLink { get; set; }

        public void WaitForYourAccountNameLink(IWebDriver browser)
        {
            var yourAccountMainPage = new YourAccountMainPage();
            PageFactory.InitElements(browser, yourAccountMainPage);
        }
    }
}
