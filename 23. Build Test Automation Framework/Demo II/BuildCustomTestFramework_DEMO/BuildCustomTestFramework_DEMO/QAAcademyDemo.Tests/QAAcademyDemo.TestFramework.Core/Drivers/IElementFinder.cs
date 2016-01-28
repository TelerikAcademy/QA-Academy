namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    using System.Collections.Generic;

    using Controls;

    public interface IElementFinder
    {
        TElement Find<TElement>(By by)
            where TElement : class, IElement;

        IEnumerable<TElement> FindAll<TElement>(By by)
            where TElement : class, IElement;
    }
}