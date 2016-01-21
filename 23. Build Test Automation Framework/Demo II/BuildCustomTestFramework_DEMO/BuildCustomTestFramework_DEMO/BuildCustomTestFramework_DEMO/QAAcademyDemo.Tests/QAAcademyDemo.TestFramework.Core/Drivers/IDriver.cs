namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public interface IDriver : IBrowser, ICookieService, IElementFinder, IJavaScriptInvoker, INavigator
    {
    }
}
