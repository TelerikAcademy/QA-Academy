namespace QAAcademyDemo.TestFramework.Core.Drivers
{
    public class By
    {
        public By(SearchCriteria searchCriteria, string value)
        {
            this.SearchCriteria = searchCriteria;
            this.SearchCriteriaValue = value;
        }

        public SearchCriteria SearchCriteria { get; set; }

        public string SearchCriteriaValue { get; set; }

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