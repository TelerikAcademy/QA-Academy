using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;

namespace TelerikAcademyWebDriver.Advanced.Telerik.Pages
{
    public class LoginPage
    {
        private readonly string loginPageUrl = @"http://www.telerik.com/";

        [FindsBy(How = How.Id, Using = "username")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "LoginButton")]
        public IWebElement LoginButton { get; set; }

        [FindsBy(How = How.Id, Using = "RememberMe")]
        public IWebElement RememberMe { get; set; }

        public void Navigate(IWebDriver browser)
        {
            browser.Navigate().GoToUrl(loginPageUrl);
            MainNavigationPage navigation = new MainNavigationPage();
            PageFactory.InitElements(browser, navigation);
            navigation.YourAccountLink.Click();
        }

        public void LoginUser(IWebDriver browser)
        {
            PageFactory.InitElements(browser, this);
            this.UserName.Clear();
            this.UserName.SendKeys("anton.angelov@telerik.com");
            this.Password.Clear();
            this.Password.SendKeys("12345");
            this.RememberMe.Click();
            this.LoginButton.Click();
        }
    }
}
