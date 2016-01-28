namespace QAAcademyDemo.Telerik.Core.Pages_01
{
    using OpenQA.Selenium;

    public abstract class BasePage<TMap, TAsserter>
        where TMap : BaseElementMap, new()
        where TAsserter : class, new()
    {
        public BasePage(IWebDriver driver)
        {
            this.Map = new TMap();
            this.Asserter = new TAsserter();
            this.Driver = driver;
        }

        public TMap Map { get; }

        public TAsserter Asserter { get; }

        public IWebDriver Driver { get; }

        public abstract void Navigate();
    }
}