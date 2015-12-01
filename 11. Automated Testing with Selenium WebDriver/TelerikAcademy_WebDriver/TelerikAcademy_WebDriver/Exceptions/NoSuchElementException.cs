using System;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace TelerikAcademyWebDriver.Exceptions
{
    public class NoSuchElementException : ApplicationException
    {
        public NoSuchElementException()
        {
        }

        public NoSuchElementException(By by, BaseWebDriverTest ext, Exception ex)
        {
            string message = this.BuildNotFoundElementExceptionText(by, ext);

            throw new ApplicationException(message, ex);
        }

        public NoSuchElementException(By by, BaseWebDriverTest ext)
        {
            string message = this.BuildNotFoundElementExceptionText(by, ext);

            throw new ApplicationException(message);
        }

        private string BuildNotFoundElementExceptionText(By by, BaseWebDriverTest ext)
        {
            StringBuilder sb = new StringBuilder();

            string customLoggingMessage =
                String.Format("#### The element with the location strategy:  {0} ####\n ####NOT FOUND!####",
                    by.ToString());
            sb.AppendLine(customLoggingMessage);

            string cuurentUrlMessage = String.Format("The URL when the test failed was: {0}", ext.Browser.Url);
            sb.AppendLine(cuurentUrlMessage);

            return sb.ToString();
        }
    }
}