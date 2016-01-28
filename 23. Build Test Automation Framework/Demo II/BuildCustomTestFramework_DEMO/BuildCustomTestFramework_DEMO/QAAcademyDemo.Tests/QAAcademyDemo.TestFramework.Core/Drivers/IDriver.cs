namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    public interface IDriver : IBrowser, ICookieService, IElementFinder, IJavaScriptInvoker, INavigator
    {
    }
}