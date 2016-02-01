namespace TestFramework.Selenium.Core
{
    using System;

    using QAAcademyDemo.TestFramework.Core.Drivers;

    public partial class SeleniumDriver : INavigator
    {
        public void Navigate(string absoluteUrl)
        {
            throw new NotImplementedException();
        }

        public void WaitForPartialUrl(string partialUrl)
        {
            throw new NotImplementedException();
        }

        public string Url
        {
            get { throw new NotImplementedException(); }
        }
    }
}