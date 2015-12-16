using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;

using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.WebAii.Controls.Html;

namespace TelerikAcademyTestProject
{
    /// <summary>
    /// Summary description for TelerikVSUnitTest1
    /// </summary>
    [TestClass]
    public class TelerikVSUnitTest1 : BaseTest
    {

        #region [Setup / TearDown]

        private TestContext testContextInstance = null;
        /// <summary>
        ///Gets or sets the VS test context which provides
        ///information about and functionality for the
        ///current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }


        // Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            #region WebAii Initialization

            // Initializes WebAii manager to be used by the test case.
            // If a WebAii configuration section exists, settings will be
            // loaded from it. Otherwise, will create a default settings
            // object with system defaults.
            //
            // Note: We are passing in a delegate to the VisualStudio
            // testContext.WriteLine() method in addition to the Visual Studio
            // TestLogs directory as our log location. This way any logging
            // done from WebAii (i.e. Manager.Log.WriteLine()) is
            // automatically logged to the VisualStudio test log and
            // the WebAii log file is placed in the same location as VS logs.
            //
            // If you do not care about unifying the log, then you can simply
            // initialize the test by calling Initialize() with no parameters;
            // that will cause the log location to be picked up from the config
            // file if it exists or will use the default system settings (C:\WebAiiLog\)
            // You can also use Initialize(LogLocation) to set a specific log
            // location for this test.

            // Pass in 'true' to recycle the browser between test methods

            Settings settings = GetSettings();
            settings.Web.DefaultBrowser = BrowserType.InternetExplorer;
            settings.Web.RecycleBrowser = true; //True is needed because of the execution of ShutDown() in ClassCleanUp(). False means that each test will be run in a separate browser
            settings.ClientReadyTimeout = 60000;
            settings.ExecuteCommandTimeout = 60000;
            settings.ExecutionDelay = 200;
            settings.AnnotateExecution = true;
            settings.AnnotationMode = AnnotationMode.All;

            Initialize(settings, new TestContextWriteLine(this.TestContext.WriteLine));

            // If you need to override any other settings coming from the
            // config section you can comment the 'Initialize' line above and instead
            // use the following:

            /*

            // This will get a new Settings object. If a configuration
            // section exists, then settings from that section will be
            // loaded

            Settings settings = GetSettings();

            // Override the settings you want. For example:
            settings.DefaultBrowser = BrowserType.FireFox;

            // Now call Initialize again with your updated settings object
            Initialize(settings, new TestContextWriteLine(this.TestContext.WriteLine));

            */

            // Set the current test method. This is needed for WebAii to discover
            // its custom TestAttributes set on methods and classes.
            // This method should always exist in [TestInitialize()] method.
            SetTestMethod(this, (string)TestContext.Properties["TestName"]);

            #endregion

            //
            // Place any additional initialization here
            //
          

        }


        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {

            //
            // Place any additional cleanup here
            //

            #region WebAii CleanUp

            // Shuts down WebAii manager and closes all browsers currently running
            // after each test. This call is ignored if recycleBrowser is set
            this.CleanUp();

            #endregion
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            // This will shut down all browsers if
            // recycleBrowser is turned on. Else
            // will do nothing.
            ShutDown();
        }

        #endregion


        [TestMethod]
        public void SampleWebAiiTest()
        {
            // Launch a browser instance
            Manager.LaunchNewBrowser(BrowserType.InternetExplorer);

            // Navigate the active browser to www.wikipedia.org
            ActiveBrowser.NavigateTo("http://www.wikipedia.org/");

            // Find the wikipedia search box and set it to "Telerik Test Studio";
            Find.ById<HtmlInputSearch>("searchInput").Text = "Telerik Test Studio";

            // Click the search arrow button
            Find.ByName<HtmlButton>("go").Click();

            // Validate that search contains 'Telerik Test Studio'
            Assert.IsTrue(ActiveBrowser.ContainsText("Telerik Test Studio"));

            //Read more here:
            //http://www.telerik.com/automated-testing-tools/support/documentation/user-guide/write-tests-in-code/intermediate-topics/element-identification/finding-page-elements.aspx
        }

        [TestMethod]
        [DataSource("BoundaryValuesDatasource")]
        [DeploymentItem("TelerikAcademyTestProject\\Data.xlsx")]
        public void Slider_DataBinding()
        {
            string minimumValue = TestContext.DataRow["MinimumValue"].ToString();
            string maximumValue = TestContext.DataRow["MaximumValue"].ToString();
            string selectionStart = TestContext.DataRow["SelectionStart"].ToString();
            string selectionEnd = TestContext.DataRow["SelectionEnd"].ToString();
            string expectedSliderStart = TestContext.DataRow["ExpectedSliderStart"].ToString();
            string expectedSliderEnd = TestContext.DataRow["ExpectedSliderEnd"].ToString();

            string configuratorId = "ctl00_ConfiguratorPlaceholder_ConfigurationPanel1";

            Manager.LaunchNewBrowser();
            Manager.ActiveBrowser.Window.Maximize();
            ActiveBrowser.NavigateTo("http://demos.telerik.com/aspnet-ajax/slider/examples/clientsideapi/defaultcs.aspx");
            HtmlDiv configurator = Find.ByAttributes<HtmlDiv>("class=panel configurator");
            //Find.ByAttributes<HtmlDiv>("class=demo-containers").ScrollToVisible();
            Manager.ActiveBrowser.ScrollBy(0, 150);

            Find.ById<HtmlInputText>(configuratorId + "_SmallChangeNtb").MouseClick();
            Find.ById<HtmlInputText>(configuratorId + "_SmallChangeNtb").Value = "1";

            Find.ById<HtmlInputText>(configuratorId + "_MinValueNtb").MouseClick();
            Find.ById<HtmlInputText>(configuratorId + "_MinValueNtb").Value = minimumValue;

            Find.ById<HtmlInputText>(configuratorId + "_MaxValueNtb").MouseClick();
            Find.ById<HtmlInputText>(configuratorId + "_MaxValueNtb").Value = maximumValue;

            Find.ById<HtmlInputText>(configuratorId + "_SelectionStartNtb").MouseClick();
            Find.ById<HtmlInputText>(configuratorId + "_SelectionStartNtb").Value = selectionStart;

            Find.ById<HtmlInputText>(configuratorId + "_SelectionEndNtb").MouseClick();
            Find.ById<HtmlInputText>(configuratorId + "_SelectionEndNtb").Value = selectionEnd;
            Find.ById<HtmlInputText>(configuratorId + "_SelectionStartNtb").MouseClick();

            RadSlider slider = Find.ById<RadSlider>("RadSliderWrapper_ctl00_ContentPlaceholder1_RadSlider1");
            Assert.AreEqual(expectedSliderStart, slider.SelectionStart.ToString());
            Assert.AreEqual(expectedSliderEnd, slider.SelectionEnd.ToString());
        }

    }
}
