using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace QAExam2015_Citypedia.Pages
{
	public partial class CityInfoPage
	{
		[FindsBy(How = How.XPath, Using = "//font[@class='sub2']/../../../tr[1]/td[2]/font/font/b")]
		public IWebElement CityName { get; set; }

		[FindsBy(How = How.XPath, Using = "//font[@class='sub2']/../../../tr[2]/td[2]/font/b")]
		public IWebElement State { get; set; }

		[FindsBy(How = How.XPath, Using = "//font[@class='sub2']/../../../tr[3]/td[2]/font/font/b/b")]
		public IWebElement ZipCode { get; set; }

		[FindsBy(How = How.XPath, Using = "//font[@class='sub2']/../../../tr[8]/td[2]/font/b")]
		public IWebElement Longitude { get; set; }

		[FindsBy(How = How.XPath, Using = "//font[@class='sub2']/../../../tr[9]/td[2]/font/b")]
		public IWebElement Latitude { get; set; }

		public By CityTableLocator
		{
			get
			{
				return By.XPath("//font[@class='sub2']");
			}
		}
	}
}
