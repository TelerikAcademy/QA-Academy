namespace TestFramework.Selenium.Core
{
    using System;

    using QAAcademyDemo.TestFramework.Core.Drivers;

    public partial class SeleniumDriver : ICookieService
    {
        public void AddCookie(string name, string value, string host)
        {
            throw new NotImplementedException();
        }

        public void DeleteCookie(string name)
        {
            throw new NotImplementedException();
        }
    }
}