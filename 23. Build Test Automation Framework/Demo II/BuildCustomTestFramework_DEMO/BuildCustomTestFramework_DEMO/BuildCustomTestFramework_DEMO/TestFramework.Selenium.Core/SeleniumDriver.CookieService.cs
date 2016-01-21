using QAAcademyDemo.TestFramework.Core.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Selenium.Core
{
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
