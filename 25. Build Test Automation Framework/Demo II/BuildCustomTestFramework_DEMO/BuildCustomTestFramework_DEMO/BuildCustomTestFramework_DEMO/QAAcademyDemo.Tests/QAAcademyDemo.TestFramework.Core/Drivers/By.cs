using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    public class By
    {
        public SearchCriteria SearchCriteria { get; set; }

        public string SearchCriteriaValue { get; set; }

        public By(SearchCriteria searchCriteria, string value)
        {
            this.SearchCriteria = searchCriteria;
            this.SearchCriteriaValue = value;
        }

        public static By XPath(string value)
        {
            return new By(SearchCriteria.XPath, value);
        }

        public static By Id(string value)
        {
            return new By(SearchCriteria.Id, value);
        }
    }
}