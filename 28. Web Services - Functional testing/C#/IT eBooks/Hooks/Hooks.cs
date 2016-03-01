using ArtOfTest.WebAii.Core;
using System.Linq;
using TechTalk.SpecFlow;

namespace ITeBooks
{
    [Binding]
    public class Hooks
    {
        public static Manager MyManager { get; set; }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            MyManager = new Manager(false);
            MyManager.Settings.Web.RecycleBrowser = true;
            MyManager.Settings.ExecutionDelay = 0;
            MyManager.Start();            
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (ScenarioContext.Current.ScenarioInfo.Tags.Contains("WebTest"))
            {
                MyManager.LaunchNewBrowser();
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if ((ScenarioContext.Current.ScenarioInfo.Tags.Contains("WebTest"))
                && (ScenarioContext.Current.TestError != null))
            {
                Screenshot.GetFullScreen(ScenarioContext.Current.ScenarioInfo.Title, true);
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            MyManager.Dispose();
        }
    }
}
