using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using QAExam2015_Citypedia.Proxy;
using System.Drawing.Imaging;
using System.Threading;
using OpenQA.Selenium.Chrome;
using QAExam2015_Citypedia.Pages;

namespace QAExam2015_Citypedia
{
	[TestClass]
	public class CityZipTest : BaseTest
	{
		private string baseUrl = "http://www.findazip.com/";
		private string cityListUrl = "http://www.findazip.com/codes-by-city.html?letter=a";
		private string screenshotsPath = @"..\..\..\Screenshots\";
		private string adBlockPath = @"..\..\..\Extensions\adblock_firefox.xpi";
		private string adBlockChromePath = @"..\..\..\Extensions\adblock_chrome.crx";
		private string chromeDriverPath = @"..\..\..\Extensions";

		[TestMethod]
		public void TakeScreenshotsByCityCoordinates()
		{
			FirefoxProfile profile = new FirefoxProfile();
			profile.AddExtension(this.adBlockPath);
			this.driver = new FirefoxDriver(profile);

			//ChromeOptions options = new ChromeOptions();
			//options.AddExtension(this.adBlockChromePath);
			//this.driver = new ChromeDriver(this.chromeDriverPath, options);

			this.driver.Manage().Window.Maximize();
			this.driver.Url = this.baseUrl;
			this.driver.Url = this.cityListUrl;

			CityListPage cityListPage = new CityListPage();
			this.WaitForElement(cityListPage.ResultsTableLocator);
			IWebElement[] links = this.driver.FindElements(cityListPage.ResultsTableLocator).ToArray();

			List<string> linkUrls = new List<string>();
			for (int i = 0; i < 10; i++)
			{
				linkUrls.Add(links[i].GetAttribute("href"));
			}

			CityInfoPage cityInfoPage = new CityInfoPage();
			GoogleMapsPage googleMapsPage = new GoogleMapsPage();
			foreach (var linkUrl in linkUrls)
			{
				this.driver.Url = linkUrl;
				this.WaitForElement(cityInfoPage.CityTableLocator);

				CityInfo info = cityInfoPage.GetCityInfo(this.driver);
				
				this.driver.Url = info.GoogleMapsLink;
				this.WaitForElement(googleMapsPage.StreetViewControl);
				this.TakeScreenshot(this.screenshotsPath + info.ToString() + ".jpg");
			}

			this.driver.Dispose();
		}

		private void TakeScreenshot(string path) 
		{
			try
			{
				Screenshot ss = ((ITakesScreenshot)this.driver).GetScreenshot();
				ss.SaveAsFile(path, ImageFormat.Jpeg);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}
	}
}
