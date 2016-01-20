namespace QAAcademyDemo.TestFramework.Core.Controls
{
    using QAAcademyDemo.TestFramework.Core.Drivers;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public interface IElement : IElementFinder
    {
        void Click();

        int Width { get; }

        bool Visible { get; }

        void MouseClick();
    }
}
