using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Android;
using System.Reflection;
using System.IO;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace AppiumDemo
{
    [TestClass]
    public class NativeApp
    {
        private static AppiumLocalService service;
        private static AppiumDriver<AndroidElement> driver;

        private TestContext testContextInstance;

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

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            // Start Appium Server
            var avdOptions = new KeyValuePair<string, string>("--avd", "Emulator-Api19-Default");
            var avdParamsOptions = new KeyValuePair<string, string>("--avd-args", "\"-scale 0.50\"");
            OptionCollector args = new OptionCollector();
            args.AddArguments(avdOptions);
            args.AddArguments(avdParamsOptions);

            service = new AppiumServiceBuilder().WithArguments(args).UsingAnyFreePort().Build();
            service.Start();
            Assert.IsTrue(service.IsRunning);

            // Start Appium Client
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "Android Emulator");
            capabilities.SetCapability("platformName", "Android");
            capabilities.SetCapability("app", "C:\\Git\\qa-academy\\2015\\MobileTesting\\testapp\\android-rottentomatoes-demo-debug.apk");
            driver = new AndroidDriver<AndroidElement>(service.ServiceUrl, capabilities);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver.Quit();
            service.Dispose();
        }

        [TestInitialize]
        public void TestInit()
        {
            driver.ResetApp();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        private static AndroidElement ListView
        {
            get
            {
                return driver.FindElementByClassName("android.widget.ListView");
            }
        }

        private static ReadOnlyCollection<AndroidElement> ListViewItems
        {
            get
            {
                return driver.FindElementsByXPath("//android.widget.ListView/android.widget.RelativeLayout");
            }
        }

        private static AndroidElement Score
        {
            get
            {
                return driver.FindElementById("com.codepath.example.rottentomatoes:id/tvAudienceScore");
            }
        }

        private static void HomeLoaded()
        {
            Assert.IsNotNull(ListView, "Home page not loaded.");
        }

        private static void DetailsLoaded()
        {
            Assert.IsNotNull(Score, "Home page not loaded.");
        }

        enum Directions { Up, Down };

        private static void Swipe(Directions direction)
        {
            var height = driver.Manage().Window.Size.Height;
            var width = driver.Manage().Window.Size.Width;

            int startX = width / 2;
            int startY = height / 2;
            int endX = width / 2;
            int endY = height / 2;

            if (direction == Directions.Down)
            {
                startY = Convert.ToInt32(height * 0.75);
                endY = Convert.ToInt32(height * 0.25);
            }
            else if (direction == Directions.Up)
            {
                startY = Convert.ToInt32(height * 0.25);
                endY = Convert.ToInt32(height * 0.75);
            }

            driver.Swipe(startX, startY, endX, endY, 1000);
        }

        [TestMethod]
        public void MasterDetailNavigation()
        {
            HomeLoaded();
            for (int i = 0; i < 3; i++)
            {
                ListViewItems[i].Click();
                DetailsLoaded();
                driver.Navigate().Back();
                HomeLoaded();
            }
        }

        [TestMethod]
        public void SwipeUpAndDown()
        {
            HomeLoaded();
            Swipe(Directions.Down);
            Swipe(Directions.Up);
            Swipe(Directions.Down);
        }

        [TestMethod]
        public void RunAppInBackground()
        {
            HomeLoaded();
            ListViewItems[1].Click();
            DetailsLoaded();
            driver.BackgroundApp(10);
            DetailsLoaded();
        }
    }
}
