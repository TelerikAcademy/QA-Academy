namespace TestFramework.Selenium.Core.Controls
{
    using OpenQA.Selenium;
    using QAAcademyDemo.TestFramework.Core.Controls;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Checkbox : Element, ICheckbox
    {
        public Checkbox(IWebElement element) : base(element)
        {
        }

        public bool IsChecked
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Check(bool shouldCheck)
        {
            throw new NotImplementedException();
        }
    }
}