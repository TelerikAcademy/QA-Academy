using Telerik.TestingFramework.Controls.KendoUI;
using Telerik.WebAii.Controls.Html;
using Telerik.WebAii.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;

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

    public class CreateNewBranch : BaseWebAiiTest
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
    
        //[CodedStep(@"Keyboard (KeyPress) - Enter (1 times) on 'ContextCommitishFilterFieldText'")]
        //public void NewBranch_CodedStep()
        //{
            //Manager.Desktop.KeyBoard.KeyPress(Keys.Enter,150,1);
        //}
    
        [CodedStep(@"Keyboard (KeyPress) - Enter (1 times) on 'ContextCommitishFilterFieldText'")]
        public void NewBranch_CodedStep1()
        {
            ActiveBrowser.Manager.Desktop.KeyBoard.KeyPress(ArtOfTest.WebAii.Win32.KeyBoard.KeysFromString("Enter"), 150, 1);
            
            
        }
    
        [CodedStep(@"Navigate to : 'https://github.com/'")]
        public void NewBranch_CodedStep2()
        {
            // Navigate to : 'https://github.com/'
            ActiveBrowser.NavigateTo("https://github.com/aurelova/QATrack", true);
            
        }
    
        //[CodedStep(@"Verify 'InnerText' 'Exact' '2branches' on 'BranchesLink'")]
        //public void NewBranch_CodedStep()
        //{
            //// Verify 'InnerText' 'Exact' '2branches' on 'BranchesLink'
            //Pages.AurelovaAaaa.BranchesLink.AssertContent().InnerText(ArtOfTest.Common.StringCompareType.Exact, "2branches");
            
        //}
    
        //[CodedStep(@"Verify 'InnerText' 'Exact' '2branches' on 'BranchesLink'")]
        //public void NewBranch_CodedStep()
        //{
            //// Verify 'InnerText' 'Exact' '2branches' on 'BranchesLink0'
            //Pages.AurelovaAaaa.BranchesLink0.AssertContent().InnerText(ArtOfTest.Common.StringCompareType.Exact, "2branches");
            
        //}
    }
}
