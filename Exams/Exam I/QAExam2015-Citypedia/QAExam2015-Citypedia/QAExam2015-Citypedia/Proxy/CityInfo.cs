using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAExam2015_Citypedia.Proxy
{
	public class CityInfo
	{
		public string Name { get; set; }

		public string State { get; set; }

		public string ZipCode { get; set; }

		public string Longitude { get; set; }

		public string Latitude { get; set; }

		public string GoogleMapsLink
		{
			get 
			{
				return string.Format("https://www.google.com/maps/preview/@{0},{1},8z", this.Latitude, this.Longitude);
			}
		}		

		public override string ToString()
		{
			return string.Format("{0}-{1}-{2}", this.Name, this.State, this.ZipCode);
		}
	}
}
