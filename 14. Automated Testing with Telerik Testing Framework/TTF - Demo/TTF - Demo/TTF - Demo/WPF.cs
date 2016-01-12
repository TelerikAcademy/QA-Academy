using System;
using System.Collections.Generic;
using System.IO;
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
using ArtOfTest.WebAii.Controls.Xaml.Wpf;
using ArtOfTest.WebAii.Wpf;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.WebAii.Controls.Xaml.Wpf;
using ArtOfTest.Common;

namespace UnitTestProject1
{
    /// <summary>
    /// Summary description for TelerikVSUnitTest2
    /// </summary>
    [TestClass]
    public class WPF : BaseWpfTest
    {
        readonly string appLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Application\WPF_DataFrom.exe";

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

            Initialize(this.TestContext.TestLogsDir, new TestContextWriteLine(this.TestContext.WriteLine));

            // If you need to override any other settings coming from the
            // config section you can comment the 'Initialize' line above and instead
            // use the following:

            /*

            // This will get a new Settings object. If a configuration
            // section exists, then settings from that section will be
            // loaded

            Settings settings = GetSettings();

            // Override the settings you want. For example:
            settings.WaitCheckInterval = 10000;

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

            // Shuts down WebAii manager and closes all applications currently running
            // after each test.
            this.CleanUp();

            #endregion
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            // This will shut down all applications
            ShutDown();
        }

        #endregion

        [TestMethod]
        public void AddTeamMember()
        {
            // Start new application
            var application = Manager.
                LaunchNewApplication(new System.Diagnostics.ProcessStartInfo(appLocation));

            // Find Textbox by Name and set its text
            application.MainWindow.Find.ByName<TextBox>("personNameTextBox").SetText(false,"Geri",10,10,true);

            // Find DateTimePicker by AutomationId and Type text in it.
            var yearBorPicker = application.MainWindow.Find.ByAutomationId<RadDateTimePicker>("yearBornPicker");
            yearBorPicker.User.Click();
            Manager.Current.Desktop.KeyBoard.TypeText("08/10/1988", 10);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Return, 10);

            // Find ComboBox by Name and call its method for selecting item
            application.MainWindow.Find.ByName<RadComboBox>("jobCombo").SelectItem("QA");
            application.MainWindow.RefreshVisualTrees();
            // Find GridView by type
            var gridView = application.MainWindow.Find.ByType<RadGridView>();

            // Get the row of the GridView which first cell contains text "Geri" and assert it is not found
            var geriRow = gridView.Rows.FirstOrDefault(row => row.Cells[0].Text == "Geri");
            Assert.IsNull(geriRow, "There is already a team member with name Geri");

            // Find the submit button by TextContent
            var submitButton = application.MainWindow.Find.ByTextContent("Submit");
            //Use the Mouse class to click on the center point of the element
            Manager.Current.Desktop.Mouse.Click(MouseClickType.LeftClick, submitButton.GetScreenRectangle());

            gridView.Refresh();
            // Get the row of the GridView which first cell contains text "Geri" and 
            // assert that it is now found and it is added to the last possition
            geriRow = gridView.Rows.FirstOrDefault(row => row.Cells[0].Text == "Geri");
            Assert.IsNotNull(geriRow, "Geri row is not present in the GridView");
            Assert.AreEqual(gridView.Rows.Count -1, gridView.Rows.IndexOf(geriRow), "The new team member is not placed on the last possition.");
        }

        [TestMethod]
        public void ReorderMembers()
        {
            // Start new application
            var application = Manager.
                LaunchNewApplication(new System.Diagnostics.ProcessStartInfo(appLocation));

            // Find GridView by type
            var gridView = application.MainWindow.Find.ByType<RadGridView>();

            // Find the Joro and Vladi rows and asserts row index
            var joroRow = gridView.Rows.FirstOrDefault(row => row.Cells[0].Text == "Joro");
            Assert.IsNotNull(joroRow, "Joro row is not present in the GridView");
            Assert.AreEqual(4, gridView.Rows.IndexOf(joroRow), "Joro row is not with index 4.");

            var vladiRow = gridView.Rows.FirstOrDefault(row => row.Cells[0].Text == "Vladi");
            Assert.IsNotNull(vladiRow, "Vladi row is not present in the GridView");
            Assert.AreEqual(1, gridView.Rows.IndexOf(vladiRow), "Vladi row is not with index 1.");

            // Drag from the center of joroRow to vladiRow with offest 5px upper of its TopCenter point  
            Manager.Current.Desktop.Mouse.DragDrop(joroRow.GetScreenRectangle(), new System.Drawing.Point(0, 0), OffsetReference.AbsoluteCenter,
                vladiRow.GetScreenRectangle(), new System.Drawing.Point(0, -5), OffsetReference.TopCenter);

            // Refresh the GridView and find the Joro and Vladi rows again as new visual elements are 
            // created for them after the drop down is performed
            gridView.Refresh();
            joroRow = gridView.Rows.FirstOrDefault(row => row.Cells[0].Text == "Joro");
            vladiRow = gridView.Rows.FirstOrDefault(row => row.Cells[0].Text == "Vladi");

            // Assert Joro and Vladi rows indexes are changed due to the drag and drop operation
            Assert.AreEqual(1, gridView.Rows.IndexOf(joroRow), "Joro row is not with index 1.");
            Assert.AreEqual(2, gridView.Rows.IndexOf(vladiRow), "Vladi row is not with index 2.");
        }
    }
}
