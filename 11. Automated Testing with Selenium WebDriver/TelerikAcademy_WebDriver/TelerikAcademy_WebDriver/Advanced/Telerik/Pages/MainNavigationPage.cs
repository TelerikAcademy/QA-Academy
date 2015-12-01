using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TelerikAcademyWebDriver.Advanced.Telerik.Pages
{
    public class MainNavigationPage
    {
        [FindsBy(How = How.Id, Using = "hlYourAccount")]
        public IWebElement YourAccountLink { get; set; }
    }
}
