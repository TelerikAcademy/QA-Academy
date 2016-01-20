using QAAcademyDemo.TestFramework.Core.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Selenium.Core
{
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
