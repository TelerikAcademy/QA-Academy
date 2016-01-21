namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public interface IBrowser
    {
        void Quit();

        void LaunchNewBrowser();

        void Refresh();

        void GoBack();

        void GoForward();

        void MaximizeWindow();
    }
}
