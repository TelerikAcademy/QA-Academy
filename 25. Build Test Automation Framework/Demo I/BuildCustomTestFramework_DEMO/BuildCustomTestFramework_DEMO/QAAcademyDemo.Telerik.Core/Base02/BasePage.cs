namespace QAAcademyDemo.Telerik.Core.Pages_02
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BasePage
    {
        private readonly IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
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