using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Telerik.WebAii.Controls.Html;

namespace Specflow_Test
{
    [Binding]
    public class StepDefinition
    {
        Browser ActiveBrowser = Hookscs.Manager.ActiveBrowser;
        
        [Given(@"I am on telerik academy home page")]
        public void GivenIAmOnTelerikAcademyHomePage()
        {
            ActiveBrowser.NavigateTo("http://telerikacademy.com/");
        }

        [When(@"I search for (.*)")]
        public void WhenISearchForXAML(string searchString)
        {
            ActiveBrowser.Find.ById<HtmlInputText>("SearchTerm").Text = searchString;
            ActiveBrowser.Find.ById<HtmlInputSubmit>("SearchButton").Click();
        }

        [Then(@"I see (.*) courses and (.*) tracks")]
        public void ThenISeeCoursesAndTracks(int coursesCount, int tracksCount)
        {
            var coursesPanel = ActiveBrowser.Find.AllByAttributes<HtmlDiv>("class=panel panel-success")
                .Where(p=> p.Find.ByExpression("tagname=h3").TextContent == "Курсове").FirstOrDefault();
            var tracksPanel = ActiveBrowser.Find.AllByAttributes<HtmlDiv>("class=panel panel-success")
               .Where(p => p.Find.ByExpression("tagname=h3").TextContent == "Тракове").FirstOrDefault();

            Assert.AreEqual(coursesCount, coursesPanel.Find.AllByExpression("class=?SearchResult").Count);
            Assert.AreEqual(coursesCount, tracksPanel.Find.AllByExpression("class=?SearchResult").Count);
                
        }

        [Given(@"I am on ""(.*)"" page")]
        public void GivenIAmOnPage(string pangeUrl)
        {
            ActiveBrowser.NavigateTo(pangeUrl);
        }
            
        [When(@"Select ""(.*)"" from the first comboBox")]
        public void WhenSelectFromTheFirstComboBox(string itemToSelect)
        {
            var combo = ActiveBrowser.Find.ByExpression<HtmlSpan>("class=?k-combobox");
            combo.Find.ByAttributes<HtmlSpan>("class=k-select").Click();
            ActiveBrowser.Find.AllByExpression<HtmlListItem>("tagname=li").Where(i => i.TextContent == "itemToSelect").FirstOrDefault().Click(); ;

        }

        [Then(@"The first comboBox text is ""(.*)""")]
        public void ThenTheFirstComboBoxTextIs(string expectedText)
        {
            Assert.AreEqual(expectedText, ActiveBrowser.Find.ByExpression<HtmlSpan>("class=?k-combobox").InnerText);
        }



    }
}
