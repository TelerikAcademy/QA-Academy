using OpenQA.Selenium.Firefox;
using QAAcademyDemo.TestFramework.Core.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Selenium.Core
{
    public partial class SeleniumDriver : IBrowser
    {
        public void Quit()
        {
            this.WrapperDriver.Quit();
        }

        public void LaunchNewBrowser()
        {
            switch (this.browserSettings.BrowserType)
            {
                case Browsers.Firefox:
                    this.WrapperDriver = new FirefoxDriver();
                    break;
                case Browsers.Chrome:
                    new NotImplementedException();
                    break;
                case Browsers.IE:
                    new NotImplementedException();
                    break;
                default:
                    break;
            }
            this.WrapperDriver.Manage().Timeouts().SetPageLoadTimeout(this.browserSettings.ExecutionTimeout);
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void GoForward()
        {
            throw new NotImplementedException();
        }

        public void MaximizeWindow()
        {
            throw new NotImplementedException();
        }
    }
}
