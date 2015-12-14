using ArtOfTest.WebAii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Specflow_Test
{
    [Binding]
    public static class Hookscs
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        public static Manager Manager { get; set; }

        [BeforeTestRun]
        public static void BeforeRun()
        {
            Manager = new Manager(false);
            Manager.Settings.ExecutionDelay = 300;
            Manager.Settings.Web.RecycleBrowser = true;
            Manager.Start();
        }


        [BeforeScenario]
        public static void BeforeScenario()
        {
            Manager.LaunchNewBrowser();
        }

        [AfterScenario]
        public static void AfterScenario()
        {
        }

        [AfterTestRun]
        public static void AfterRun()
        {
            Manager.Dispose();
        }
    }
}
