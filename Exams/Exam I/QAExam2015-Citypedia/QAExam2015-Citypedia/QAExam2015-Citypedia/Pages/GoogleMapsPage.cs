using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace QAExam2015_Citypedia.Pages
{
	public class GoogleMapsPage
	{
		public By StreetViewControl
		{
			get
			{
				return By.Id("runway-expand-button");
			}
		}
	}
}
