using Telerik.TestingFramework.Controls.KendoUI;
using Telerik.WebAii.Controls.Html;
using Telerik.WebAii.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
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

    public class CreateRepository : BaseWebAiiTest
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
        //public void git_CodedStep()
        //{
            //Log.WriteLine("Exists");
        //}
    
        //[CodedStep(@"New Coded Step")]
        //public void git_CodedStep1()
        //{
            //Log.WriteLine("Exists NOT");
        //}
    
        //[CodedStep(@"Keyboard (KeyPress) - Enter (1 times) on 'ContextCommitishFilterFieldText'")]
        //public void git_CodedStep2()
        //{
            //ActiveBrowser.Manager.Desktop.KeyBoard.KeyPress(ArtOfTest.WebAii.Win32.KeyBoard.KeysFromString("Enter"), 150, 1);
            
            
        //}
    
        //[CodedStep(@"Execute test 'loginAndVerify'")]
        //public void git_CodedStep3()
        //{
            //// Execute test 'loginAndVerify'
            //this.ExecuteTest("loginAndVerify.tstest");
            
        //}
    
        //[CodedStep(@"Keyboard (KeyPress) - Enter (1 times) on 'ContextCommitishFilterFieldText'")]
        //public void git_CodedStep3()
        //{
            //Manager.Desktop.KeyBoard.KeyPress(Keys.Enter,150,1);
        //}
    
        //[CodedStep(@"New Coded Step")]
        //public void git_CodedStep4()
        //{
            
        //}
    }
}
