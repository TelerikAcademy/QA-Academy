using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace QAExam2015_Citypedia
{
	public abstract class BaseTest
	{
		private static double secondsToWait = 20;
		private static TimeSpan waitForElement = TimeSpan.FromSeconds(secondsToWait);
		internal IWebDriver driver;

		public void WaitForElement(By by)
		{
			try
			{
				WebDriverWait wait = new WebDriverWait(this.driver, waitForElement);
				wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
			}
			catch
			{
				throw new TimeoutException(string.Format("Element with locator: {0} was not found in {1} seconds!", by.ToString(), secondsToWait));
			}
		}
	}
}
