namespace TestFramework.Selenium.Core
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ByExtensions
    {
        public static By ToSeleniumBy(this QAAcademyDemo.TestFramework.Core.Drivers.By by)
        {
            By result = default(By);
            switch (by.SearchCriteria)
            {
                case QAAcademyDemo.TestFramework.Core.Drivers.SearchCriteria.Id:
                    result = By.Id(by.SearchCriteriaValue);
                    break;
                case QAAcademyDemo.TestFramework.Core.Drivers.SearchCriteria.XPath:
                    result = By.XPath(by.SearchCriteriaValue);
                    break;
                case QAAcademyDemo.TestFramework.Core.Drivers.SearchCriteria.ClassName:
                    result = By.ClassName(by.SearchCriteriaValue);
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}