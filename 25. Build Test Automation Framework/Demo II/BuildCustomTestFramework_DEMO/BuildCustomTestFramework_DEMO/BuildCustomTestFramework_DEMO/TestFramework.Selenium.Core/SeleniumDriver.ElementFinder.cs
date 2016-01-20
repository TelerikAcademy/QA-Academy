using OpenQA.Selenium;
using QAAcademyDemo.TestFramework.Core.Controls;
using QAAcademyDemo.TestFramework.Core.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Selenium.Core.Controls;

namespace TestFramework.Selenium.Core
{
    public partial class SeleniumDriver : IElementFinder
    {
        public TElement Find<TElement>(By by) where TElement : class, IElement
        {
            TElement resultElement = default(TElement);
            IWebElement seleniumElement = this.WrapperDriver.FindElement(by.ToSeleniumBy());

            if (typeof(TElement).Equals(typeof(IButton)))
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
