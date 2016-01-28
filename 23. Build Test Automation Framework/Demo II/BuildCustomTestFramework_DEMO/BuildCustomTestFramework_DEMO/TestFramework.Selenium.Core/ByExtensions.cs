namespace TestFramework.Selenium.Core
{
    using QAAcademyDemo.TestFramework.Core.Drivers;

    using By = OpenQA.Selenium.By;

    public static class ByExtensions
    {
        public static By ToSeleniumBy(this QAAcademyDemo.TestFramework.Core.Drivers.By by)
        {
            var result = default(By);
            switch (by.SearchCriteria)
            {
                case SearchCriteria.Id:
                    result = By.Id(by.SearchCriteriaValue);
                    break;
                case SearchCriteria.XPath:
                    result = By.XPath(by.SearchCriteriaValue);
                    break;
                case SearchCriteria.ClassName:
                    result = By.ClassName(by.SearchCriteriaValue);
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}