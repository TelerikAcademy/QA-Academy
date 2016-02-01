namespace QAAcademyDemo.TestFramework.Core.Drivers
{
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