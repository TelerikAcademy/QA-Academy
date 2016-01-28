namespace TestFramework.Selenium.Core
{
    using System;
    using System.Collections.Generic;

    using QAAcademyDemo.TestFramework.Core.Controls;
    using QAAcademyDemo.TestFramework.Core.Drivers;

    public partial class SeleniumDriver : IElementFinder
    {
        public TElement Find<TElement>(By by) where TElement : class, IElement
        {
            var resultElement = default(TElement);
            var seleniumElement = this.WrapperDriver.FindElement(by.ToSeleniumBy());

            if (typeof (TElement).Equals(typeof (IButton)))
            {
                ////resultElement = new Button(seleniumElement);
            }

            return resultElement;
        }

        public IEnumerable<TElement> FindAll<TElement>(By by) where TElement : class, IElement
        {
            throw new NotImplementedException();
        }
    }
}