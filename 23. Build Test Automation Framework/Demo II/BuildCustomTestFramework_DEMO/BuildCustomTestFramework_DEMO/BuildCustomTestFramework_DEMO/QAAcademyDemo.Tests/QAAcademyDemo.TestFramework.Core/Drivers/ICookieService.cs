namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public interface ICookieService
    {
        void AddCookie(string name, string value, string host);

        void DeleteCookie(string name);
    }
}
