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

namespace TTF___Demo
{
    /// <summary>
    /// Summary description for TelerikVSUnitTest1
    /// </summary>
    [TestClass]
    public class WebTests : BaseTest
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
            Initialize(false, this.TestContext.TestLogsDir, new TestContextWriteLine(this.TestContext.WriteLine));

            // If you need to override any other settings coming from the
            // config section you can comment the 'Initialize' line above and instead
            // use the following:

            /*

            // This will get a new Settings object. If a configuration
            // section exists, then settings from that section will be
            // loaded

            Settings settings = GetSettings();

            // Override the settings you want. For example:
            settings.Web.DefaultBrowser = BrowserType.FireFox;

            // Now call Initialize again with your updated settings object
            Initialize(settings, new TestContextWriteLine(this.TestContext.WriteLine));

            */

            // Set the current test method. This is needed for WebAii to discover
            // its custom TestAttributes set on methods and classes.
            // This method should always exist in [TestInitialize()] method.
            SetTestMethod(this, (string)TestContext.Properties["TestName"]);

            #endregion


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
        public void SearchXaml()
        {
            // Start browser and navigate to Telerik Academy home page
            this.Manager.LaunchNewBrowser();
            this.ActiveBrowser.NavigateTo("http://telerikacademy.com/");

            // Enter "XAML" and click Search button
            // Find elements by ID
            Find.ById<HtmlInputText>("SearchTerm").Text = "XAML";
            Find.ById<HtmlInputSubmit>("SearchButton").Click();

            // Find all elements with spesific attribute
            var allResultsCategories = this.Find.AllByAttributes<HtmlDiv>("class=panel panel-success");

            Assert.IsTrue(allResultsCategories.Count > 0,
                "No divs with class=panel panel-success were found! This could indicate that there are no results.");

            // Linq and find by TagName is used to locate the div holding the courses results
            var coursesHolder = allResultsCategories.
                Where(category => category.Find.AllByTagName("h3")
                    .FirstOrDefault().TextContent == "Курсове")
                    .FirstOrDefault();

            // Find all the courses results
            var allCourses = coursesHolder.Find.AllByAttributes<HtmlDiv>("class=col-sm-3 col-xs-6 marginBottom10 friendSearchResult");

            Assert.AreEqual(4, allCourses.Count, "The number of courses does not match!");

            // Linq and find by Expression is used to locate the div holding the courses results
            var tracksHolder = allResultsCategories.
                Where(category => category.Find.ByCustom(e => e.TagName == "h3").TextContent == "Тракове").FirstOrDefault();

            // Find the all tracks using expression
            var allTracks = tracksHolder.Find.AllByExpression<HtmlDiv>("class=?friendSearchResult");

            Assert.AreEqual(4, allTracks.Count, "The number of tracks does not match!");

            // Find the MainContainer and in it find the result text heading
            var resultText = this.Find.ByExpression("id=MainContainer", "|", "class=sectionTitle");
            Assert.AreEqual("Вашето търсене за \"XAML\" върна следните резултати", resultText.TextContent);
        }

        /// <summary>
        /// Verify user can not login with invalid user and password
        /// </summary>
        [TestMethod]
        public void InvalidLogin()
        {
            // Start browser and navigate to Telerik Academy home page
            this.Manager.LaunchNewBrowser();
            this.ActiveBrowser.NavigateTo("http://stage.telerikacademy.com/");

            // Find login button and clik it
            this.Find.ById<HtmlAnchor>("EntranceButton").Click();

            // Wait for User and Password elements
            this.ActiveBrowser.WaitForElement(new HtmlFindExpression("textcontent=Вход в системата"), 30000, false);

            // Populate user and password
            this.Find.ById<HtmlInputText>("UsernameOrEmail").Text = "fakeUser";
            this.Find.ById<HtmlInputPassword>("Password").Text = "fakePassword";

            // Click "Вход"
            var exp = new HtmlFindExpression("tagname=input", "value=Вход");
            this.Find.ByExpression<HtmlInputSubmit>(exp).Click();

            // Verity error message
            Assert.IsTrue(ActiveBrowser.ContainsText("Невалидни данни за достъп!"));
        }
    }
}
