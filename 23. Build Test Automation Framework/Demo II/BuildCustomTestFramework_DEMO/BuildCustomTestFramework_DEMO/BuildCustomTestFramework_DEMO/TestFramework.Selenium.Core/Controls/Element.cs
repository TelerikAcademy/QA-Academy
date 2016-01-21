namespace TestFramework.Selenium.Core.Controls
{
    using OpenQA.Selenium;
    using QAAcademyDemo.TestFramework.Core.Controls;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Element : IElement
    {
        public Element(IWebElement element)
        {
            this.WebElement = element;
        }

        public IWebElement WebElement { get; set; }

        public void Click()
        {
            this.WebElement.Click();
        }

        public int Width
        {
            get
            {
                return this.WebElement.Size.Width;
            }
        }

        public bool Visible
        {
            get
            {
                return this.WebElement.Displayed;
            }
        }

        public void MouseClick()
        {
            throw new NotImplementedException();
        }

        TElement QAAcademyDemo.TestFramework.Core.Drivers.IElementFinder.Find<TElement>(QAAcademyDemo.TestFramework.Core.Drivers.By by)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TElement> QAAcademyDemo.TestFramework.Core.Drivers.IElementFinder.FindAll<TElement>(QAAcademyDemo.TestFramework.Core.Drivers.By by)
        {
            throw new NotImplementedException();
        }
    }
}