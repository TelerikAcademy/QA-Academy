using TechTalk.SpecFlow;

namespace ITeBooks
{
    public static class ScenarioContextExtensions
    {
        public static void AddOrUpdate<T>(this ScenarioContext scenarioContext, string propertyName, T value)
        {
            try
            {
                scenarioContext[propertyName] = value;
            }
            catch 
            {
                scenarioContext.Add(propertyName, value);
            }
        }
    }
}
