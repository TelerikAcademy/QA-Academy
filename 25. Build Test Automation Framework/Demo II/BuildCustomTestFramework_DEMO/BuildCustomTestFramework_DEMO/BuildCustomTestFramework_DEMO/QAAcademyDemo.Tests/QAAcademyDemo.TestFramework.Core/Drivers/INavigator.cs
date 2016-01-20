namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public interface INavigator
    {
        void Navigate(string absoluteUrl);

        void WaitForPartialUrl(string partialUrl);

        string Url { get; }
    }
}
