using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.PhantomJS;

namespace TelerikAcademyWebDriver
{
    [TestClass]
    public class LoginTest : BaseWebDriverTest
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [TestInitialize]
        public void SetupTest()
        {
            ////this.TestInit(new FirefoxDriver(), @"http://www.telerik.com/", 10);

            //this.Browser = new ChromeDriver(@"D:\QA_Academy_2014\2014\Lectures\Day8_Selenium Web Driver\Solutions\Solutions\HW\TelerikAcademy_WebDriver\TelerikAcademy_WebDriver");


            DesiredCapabilities capability = DesiredCapabilities.InternetExplorer();
            System.Environment.SetEnvironmentVariable("webdriver.ie.driver", "C:\\Driver\\IEDriverServer.exe");
            this.Browser = new RemoteWebDriver(new Uri("http://192.168.54.56:4444/wd/hub"), capability);
            ////this.Browser = new PhantomJSDriver(@"D:\QA_Academy_2014\2014\Lectures\Day8_Selenium Web Driver\Demo\TelerikAcademy_WebDriver\phantomjs-1.9.7-windows");

            //this.Browser = new FirefoxDriver();
            this.BaseUrl = @"http://www.telerik.com/";
            this.Wait = new WebDriverWait(this.Browser, TimeSpan.FromSeconds(10));
            this.TimeOut = 10;

            //set different firefox profile
            ////string path = @"C:\Users\ageorgieva\AppData\Roaming\Mozilla\Firefox\Profiles\vrmbkgmi.test user profile";
            ////FirefoxProfile ffprofile = new FirefoxProfile(path);
            ////this.Browser = new FirefoxDriver(ffprofile);
            ////this.Browser.Manage().Cookies.DeleteAllCookies();
            ////this.Browser.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));

            //var p = new FirefoxProfile();
            //p.SetPreference("javascript.enabled", false); //In Firefox, go to "about:config" to see this Prefernce
            //driver = new FirefoxDriver(p);//"C:\\Software"); 
        }

        [TestCleanup]
        public void TeardownTest()
        {
            try
            {
                this.Browser.Quit();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        [TestMethod]
        public void Login_Telerik_Telerik_Provider()
        {
            this.Browser.Navigate().GoToUrl(this.BaseUrl);
            this.CurrentElement = this.GetElement((By.Id("hlYourAccount")));
            this.CurrentElement.Click();
            this.CurrentElement = this.GetElement((By.Id("username")));
            this.CurrentElement.Clear();
            this.CurrentElement.SendKeys("anton.angelov@telerik.com");
            this.CurrentElement = this.GetElement((By.Id("password")));
            this.CurrentElement.Clear();
            this.CurrentElement.SendKeys("12345");
            this.CurrentElement = this.GetElement((By.Id("RememberMe")));
            this.CurrentElement.Click();
            this.CurrentElement = this.GetElement((By.Id("LoginButton")));
            this.CurrentElement.Click();
            this.WaitForElementPresent(By.CssSelector("span.telerik-hi-user-name.js-telerik-user-first-name"));
            this.CurrentElement = this.GetElement((By.LinkText("Profile")));
            this.CurrentElement.Click();
            this.CurrentElement = this.GetElement((By.Id("ctl00_ctl00_GeneralBox_YourAccountNavigationArea_ctl00_hlEditProfile")));
            this.CurrentElement.Click();
            this.CurrentElement = this.GetElement((By.Id("ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_client_editprofile_ascx1_ucCountrySelector_rcbCountry_Arrow")));
            this.CurrentElement.Click();
            ////this.CurrentElement = this.GetElement((By.CssSelector("li.rcbHovered.")));
            ////this.CurrentElement.Click();
            ////this.CurrentElement = this.GetElement((By.Id("ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_client_editprofile_ascx1_ucCountrySelector_rcbCountry_Input")));
            ////this.CurrentElement.Clear();
            ////this.CurrentElement.SendKeys("Germany");
            this.CurrentElement = this.GetElement((By.Id("ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_client_editprofile_ascx1_scetbInterests_tbSanitized")));
            this.CurrentElement.Clear();
            this.CurrentElement.SendKeys("testInterests");
            ////this.CurrentElement = this.GetElement((By.Id("ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_client_editprofile_ascx1_lbUpdate")));
            ////this.CurrentElement.Click();
            ////this.CurrentElement = this.GetElement((By.Id("ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_client_editprofile_ascx1_ucCountrySelector_rcbCountry_Arrow")));
            ////this.CurrentElement.Click();
            ////this.CurrentElement = this.GetElement((By.CssSelector("li.rcbHovered.")));
            ////this.CurrentElement.Click();
            ////this.CurrentElement = this.GetElement((By.Id("ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_client_editprofile_ascx1_ucCountrySelector_rcbCountry_Input")));
            ////this.CurrentElement.Clear();
            ////this.CurrentElement.SendKeys("Bulgaria");
            this.CurrentElement = this.GetElement((By.Id("ctl00_ctl00_GeneralBox_Content_usercontrols_public_unitedaccount_client_editprofile_ascx1_lbUpdate")));
            this.CurrentElement.Click();
            this.CurrentElement = this.GetElement((By.LinkText("Your Account")));
            this.CurrentElement.Click();
            this.CurrentElement = this.GetElement((By.CssSelector("span.telerik-hi-user-name.js-telerik-user-first-name")));
            this.CurrentElement.Click();
            this.CurrentElement = this.GetElement((By.Id("ctl00_ctl00_siteNavigation30_functionalityControl30_hlLogout")));
            this.CurrentElement.Click();
            this.WaitForElementPresent(By.Id("hlYourAccount"));
            this.WaitForText(By.Id("hlYourAccount"), "Your Account");
        }

        [TestMethod]
        public void Chained_Element_Finders()
        {
            this.Browser.Navigate().GoToUrl(@"http://demos.telerik.com/aspnet-ajax/treeview/examples/populatingwithdata/client-side-data-binding/defaultcs.aspx?isNew=true");
            this.WaitForElementPresent(By.ClassName("qsf-content"));
            this.CurrentElement = this.Browser.FindElement(By.ClassName("qsf-content")).FindElement(By.Id("ctl00_LeftNavigationControl_ControlDemosNavigation_i1_i0_NodeText"));
            Debug.WriteLine(string.Format("Locator of the element: {0}", CurrentElement.Text));
            this.CurrentElement.Click();
        }

        [TestMethod]
        public void CheckBox_Check()
        {
            this.Browser.Navigate().GoToUrl(@"http://www.w3schools.com/html/html_forms.asp");
            this.WaitForElementPresent(By.CssSelector("#main > form:nth-child(49) > input:nth-child(1)"));
            this.CurrentElement = this.Browser.FindElement(By.CssSelector("#main > form:nth-child(49) > input:nth-child(1)"));
            this.CurrentElement.Click();
            Assert.IsTrue(this.CurrentElement.Selected);
        }

        [TestMethod]
        public void RadioButton_Click()
        {
            this.Browser.Navigate().GoToUrl(@"http://www.w3schools.com/html/html_forms.asp");
            this.WaitForElementPresent(By.XPath("//input[@value='male']"));
            this.CurrentElement = this.Browser.FindElement(By.XPath("//input[@value='male']"));
            this.CurrentElement.Click();
            Assert.IsTrue(this.CurrentElement.Selected);
        }

        [TestMethod]
        public void IFrame_DropDown_Click()
        {
            this.Browser.Navigate().GoToUrl(@"http://www.w3schools.com/tags/tryit.asp?filename=tryhtml_select");
            this.Browser = this.Browser.SwitchTo().Frame("iframeResult");
            this.WaitForElementPresent(By.XPath("/html/body/select"));
            SelectElement selectElement = new SelectElement(this.Browser.FindElement(By.XPath("/html/body/select")));
            selectElement.SelectByText("Saab");
            Assert.AreEqual<string>("Saab", selectElement.SelectedOption.Text);
        }

        [TestMethod]
        public void Find_Column_Table_XPath()
        {
            this.Browser.Navigate().GoToUrl(@"http://www.tutorialspoint.com/html/html_tables.htm");
            var expression = By.XPath("/html/body/div[1]/div/div/div/div[2]/div[1]/div/div[6]/table/tbody");
            this.WaitForElementPresent(expression);
            this.CurrentElement = this.Browser.FindElement(By.XPath("//td[contains(text(), 'Shabbir')]/following-sibling::td[1]"));
            Assert.AreEqual<string>("7000", this.CurrentElement.Text);
        }

        [TestMethod]
        public void GoogleGroups()
        {
            this.Browser.Navigate().GoToUrl(@"http://www.seleniumwebdriver.com/selenium-webdriver-resources/working-with-chrome-driver-using-c/");
        }
    }
}