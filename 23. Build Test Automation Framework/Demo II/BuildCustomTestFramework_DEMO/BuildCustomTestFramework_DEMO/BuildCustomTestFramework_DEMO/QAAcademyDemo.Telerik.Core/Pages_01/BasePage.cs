namespace QAAcademyDemo.Telerik.Core.Pages_01
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BasePage<TMap, TAsserter>
        where TMap : BaseElementMap, class, new()
        where TAsserter : class, new()
    {
        private readonly TMap map;
        private readonly TAsserter asserter;
        private readonly IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.map = new TMap(driver);
            this.asserter = new TAsserter();
            this.driver = driver;
        }

        public TMap Map 
        {
            get
            {
                return this.map;
            }
        }

        public TAsserter Asserter
        {
            get
            {
                return this.asserter;
            }
        }

        public IWebDriver Driver
        {
            get
            {
                return this.driver;
            }
        }

        public abstract void Navigate();
    }
}