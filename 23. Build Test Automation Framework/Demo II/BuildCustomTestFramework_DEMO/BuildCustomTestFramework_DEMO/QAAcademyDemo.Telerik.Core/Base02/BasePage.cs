namespace QAAcademyDemo.Telerik.Core.Base02
{
    using OpenQA.Selenium;

    public abstract class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public IWebDriver Driver { get; }

        public abstract void Navigate();
    }
}