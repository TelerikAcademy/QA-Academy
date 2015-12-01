using System;
using System.Linq;
using System.Text;

namespace TelerikAcademyWebDriver.Exceptions
{
    public class TextNotFoundException : ApplicationException
    {
        public TextNotFoundException()        
        {
        }

        public TextNotFoundException(string textToFind, BaseWebDriverTest ext, Exception ex)
        {
            string message = this.BuildTextNotFoundExceptionText(textToFind, ext);

            throw new ApplicationException(message, ex);
        }

        public TextNotFoundException(string textToFind, BaseWebDriverTest ext)
        {
            string message = this.BuildTextNotFoundExceptionText(textToFind, ext);

            throw new ApplicationException(message);
        }

        private string BuildTextNotFoundExceptionText(string textToFind, BaseWebDriverTest ext)
        {
            StringBuilder sb = new StringBuilder();

            string customLoggingMessage =
                String.Format("#### The text:  {0} ####\n ####WAS NOT FOUND!####", textToFind);
            sb.AppendLine(customLoggingMessage);

            string cuurentUrlMessage = String.Format("The URL when the test failed was: {0}", ext.Browser.Url);
            sb.AppendLine(cuurentUrlMessage);

            return sb.ToString();
        }
    }
}