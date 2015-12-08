using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace QAExam2015_Citypedia.Pages
{
	public class CityListPage
	{
		public By ResultsTableLocator
		{
			get
			{
				return By.XPath("//font[@class='sub']/../../../tr[4]/td/a"); 
			}
		}
		
	}
}
