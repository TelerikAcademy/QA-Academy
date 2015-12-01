using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using TelerikAcademyWebDriver.Advanced.Telerik.Pages;

namespace TelerikAcademyWebDriver.Advanced.Telerik.Tests
{
    [TestClass]
    public class LoginTest : BaseWebDriverTest
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [TestInitialize]
        public void SetupTest()
        {
            this.Browser = new FirefoxDriver();
            this.BaseUrl = @"http://www.telerik.com/";
            this.Wait = new WebDriverWait(this.Browser, TimeSpan.FromSeconds(10));
            this.TimeOut = 10;
        }

        [TestCleanup]
        public void TeardownTest()
        {
            try
            {
                this.Browser.Quit();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        [TestMethod]
        public void Login_Telerik_Telerik_Provider_ElementsFactory()
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Navigate(this.Browser);
            loginPage.LoginUser(this.Browser);
            YourAccountMainPage mainPage = new YourAccountMainPage();
            mainPage.WaitForYourAccountNameLink(this.Browser);
        }      
    }
}