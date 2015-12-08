using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using QAExam2015_Citypedia.Proxy;

namespace QAExam2015_Citypedia.Pages
{
	public partial class CityInfoPage
	{
		public CityInfo GetCityInfo(IWebDriver driver)
		{
			PageFactory.InitElements(driver, this);
			CityInfo info = new CityInfo()
			{
				Name = this.CityName.Text,
				State = this.State.Text,
				ZipCode = this.ZipCode.Text,
				Longitude = this.Longitude.Text,
				Latitude = this.Latitude.Text
			};

			return info;
		}
	}
}
