using OpenQA.Selenium;
using QAAcademyDemo.TestFramework.Core.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Selenium.Core
{
    public partial class SeleniumDriver
    {
        private readonly IWebDriver driver;
        private readonly BrowserSettings browserSettings;

        public SeleniumDriver(BrowserSettings settings)
        {
            this.browserSettings = settings;
        }

        public IWebDriver WrapperDriver { get; set; }
    }
}
