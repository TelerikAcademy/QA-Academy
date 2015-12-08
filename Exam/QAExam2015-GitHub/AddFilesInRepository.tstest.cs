using Telerik.TestingFramework.Controls.KendoUI;
using Telerik.WebAii.Controls.Html;
using Telerik.WebAii.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;

namespace TestProject1
{

    public class AddFilesInRepository : BaseWebAiiTest
    {
        #region [ Dynamic Pages Reference ]

        private Pages _pages;

        /// <summary>
        /// Gets the Pages object that has references
        /// to all the elements, frames or regions
        /// in this project.
        /// </summary>
        public Pages Pages
        {
            get
            {
                if (_pages == null)
                {
                    _pages = new Pages(Manager.Current);
                }
                return _pages;
            }
        }

        #endregion
        
        // Add your test methods here...
    
        //[CodedStep(@"New Coded Step")]
        //public void add_file_CodedStep()
        //{
            //Log.WriteLine(Data[0].ToString());
        //}
    
        //[CodedStep(@"Verify 'TextContent' 'Contains' '' on 'Table'")]
        //public void add_file_CodedStep1()
        //{
            //object myData = GetExtractedValue("latest");
            
            //Log.WriteLine(myData.ToString());
            //Log.WriteLine(Data[0].ToString());
            
        //}
    
        //[CodedStep(@"New Coded Step")]
        //public void add_file_CodedStep()
        //{
            //Log.WriteLine(Data[0].ToString());
        //}
    
        //[CodedStep(@"Navigate to : 'https://github.com/'")]
        //public void add_file_CodedStep()
        //{
            //// Navigate to : 'https://github.com/'
            //ActiveBrowser.NavigateTo("https://github.com/", true);
            
        //}
    
        //[CodedStep(@"Execute test 'loginAndVerify'")]
        //public void add_file_CodedStep2()
        //{
            //// Execute test 'loginAndVerify'
            //this.ExecuteTest("loginAndVerify.tstest");
            
        //}
    
        //[CodedStep(@"Clear Browser Cache")]
        //public void add_file_CodedStep3()
        //{
            //// Clear Browser Cache
            //ActiveBrowser.ClearCache(ArtOfTest.WebAii.Core.BrowserCacheType.Cookies);
            //ActiveBrowser.ClearCache(ArtOfTest.WebAii.Core.BrowserCacheType.TempFilesCache);
            //ActiveBrowser.ClearCache(ArtOfTest.WebAii.Core.BrowserCacheType.History);
            
        //}
    
        [CodedStep(@"Navigate to : 'https://github.com/'")]
        public void add_file_CodedStep4()
        {
            // Navigate to : 'https://github.com/'
            ActiveBrowser.NavigateTo("https://github.com/aurelova/QATrack", true);
            
        }
    
        //[CodedStep(@"New Coded Step")]
        //public void git_CodedStep()
        //{
            //Log.WriteLine("Exists");
        //}
    
        //[CodedStep(@"New Coded Step")]
        //public void git_CodedStep1()
        //{
            //Log.WriteLine("Exists NOT");
        //}
    
        //[CodedStep(@"New Coded Step")]
        //public void add_file_CodedStep()
        //{
            //Log.WriteLine("Exists");
        //}
    
        //[CodedStep(@"Navigate to : 'https://github.com/'")]
        //public void add_file_CodedStep1()
        //{
            //// Navigate to : 'https://github.com/'
            //ActiveBrowser.NavigateTo("https://github.com/aurelova/qa2", true);
            
        //}
    
        //[CodedStep(@"Navigate to : 'https://github.com/'")]
        //public void add_file_CodedStep1()
        //{
            //// Navigate to : 'https://github.com/'
            //ActiveBrowser.NavigateTo("https://github.com/aurelova/qa2", true);
            
        //}
    }
}
