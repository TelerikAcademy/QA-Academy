namespace QAAcademyDemo.Telerik.Core.Pages_01
{
    using OpenQA.Selenium;

    public class BaseElementMap
    {
        public BaseElementMap(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public BaseElementMap()
        {
        }

        protected IWebDriver Driver { get; set; }
    }
}