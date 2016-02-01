namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    public interface ICookieService
    {
        void AddCookie(string name, string value, string host);

        void DeleteCookie(string name);
    }
}