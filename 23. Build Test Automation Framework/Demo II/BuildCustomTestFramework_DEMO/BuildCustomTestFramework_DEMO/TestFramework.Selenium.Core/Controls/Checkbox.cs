namespace TestFramework.Selenium.Core.Controls
{
    using System;

    using OpenQA.Selenium;

    using QAAcademyDemo.TestFramework.Core.Controls;

    public class Checkbox : Element, ICheckbox
    {
        public Checkbox(IWebElement element) : base(element)
        {
        }

        public bool IsChecked
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void Check(bool shouldCheck)
        {
            throw new NotImplementedException();
        }
    }
}