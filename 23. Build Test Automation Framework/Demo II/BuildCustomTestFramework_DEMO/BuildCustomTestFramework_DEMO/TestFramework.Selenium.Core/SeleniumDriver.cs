namespace TestFramework.Selenium.Core
{
    using OpenQA.Selenium;

    using QAAcademyDemo.TestFramework.Core;

    public partial class SeleniumDriver
    {
        private readonly BrowserSettings browserSettings;
        private readonly IWebDriver driver;

        public SeleniumDriver(BrowserSettings settings)
        {
            this.browserSettings = settings;
        }

        public IWebDriver WrapperDriver { get; set; }
    }
}