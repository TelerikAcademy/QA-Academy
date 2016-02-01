namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    public interface INavigator
    {
        string Url { get; }
        void Navigate(string absoluteUrl);

        void WaitForPartialUrl(string partialUrl);
    }
}