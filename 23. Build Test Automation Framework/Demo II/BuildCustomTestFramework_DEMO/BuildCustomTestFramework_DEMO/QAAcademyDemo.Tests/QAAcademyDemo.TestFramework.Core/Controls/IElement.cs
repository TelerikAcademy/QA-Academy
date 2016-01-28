namespace QAAcademyDemo.TestFramework.Core.Controls
{
    using Drivers;

    public interface IElement : IElementFinder
    {
        int Width { get; }

        bool Visible { get; }
        void Click();

        void MouseClick();
    }
}