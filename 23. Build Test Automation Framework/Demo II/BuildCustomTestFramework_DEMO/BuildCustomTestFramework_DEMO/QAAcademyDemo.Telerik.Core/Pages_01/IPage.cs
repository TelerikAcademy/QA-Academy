namespace QAAcademyDemo.Telerik.Core.Pages_01
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public interface IPage<TMap, TAsserter>
        where TMap : BaseElementMap, class, new()
        where TAsserter : class, new()
    {
        void Navigate();

        TMap Map { get;  }

        TAsserter Asserter { get; }

        IWebDriver Driver { get; }
    }
}
