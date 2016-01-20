namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    using QAAcademyDemo.TestFramework.Core.Controls;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IElementFinder
    {
        TElement Find<TElement>(By by)
            where TElement : class, IElement;

        IEnumerable<TElement> FindAll<TElement>(By by)
            where TElement : class, IElement;
    }
}