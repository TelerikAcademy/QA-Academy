namespace QAAcademyDemo.Telerik.Core.Pages_01
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BaseElementMap
    {
        private readonly IWebDriver driver;

        public BaseElementMap(IWebDriver driver)
        {
            this.driver = driver;
        }

        protected IWebDriver Driver
        {
            get
            {
                return this.driver;
            }
        }
    }
}