namespace TestFramework.Selenium.Core.Controls
{
    using System;
    using System.Collections.Generic;

    using OpenQA.Selenium;

    using QAAcademyDemo.TestFramework.Core.Controls;
    using QAAcademyDemo.TestFramework.Core.Drivers;

    using By = QAAcademyDemo.TestFramework.Core.Drivers.By;

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
            get { return this.WebElement.Size.Width; }
        }

        public bool Visible
        {
            get { return this.WebElement.Displayed; }
        }

        public void MouseClick()
        {
            throw new NotImplementedException();
        }

        TElement IElementFinder.Find<TElement>(By by)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TElement> IElementFinder.FindAll<TElement>(By by)
        {
            throw new NotImplementedException();
        }
    }
}