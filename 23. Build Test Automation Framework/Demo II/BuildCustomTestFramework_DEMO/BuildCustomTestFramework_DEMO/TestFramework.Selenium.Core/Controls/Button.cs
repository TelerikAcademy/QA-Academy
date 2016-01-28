namespace TestFramework.Selenium.Core.Controls
{
    using System;

    using OpenQA.Selenium;

    using QAAcademyDemo.TestFramework.Core.Controls;

    public class Button : ContentElement, IButton
    {
        public Button(IWebElement element) : base(element)
        {
        }

        public bool IsEnabled
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}