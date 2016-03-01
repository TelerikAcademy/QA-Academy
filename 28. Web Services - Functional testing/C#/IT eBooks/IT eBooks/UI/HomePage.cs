using ArtOfTest.WebAii.Controls.HtmlControls;
using System.Collections.ObjectModel;

namespace ITeBooks
{
    public static class HomePage
    {
        private static HtmlInputText SearchBox
        {
            get
            {
                return BAT.Browser.Find.ById<HtmlInputText>("q");
            }
        }

        private static HtmlInputSubmit SearchButton
        {
            get
            {
                return BAT.Browser.Find.ByAttributes<HtmlInputSubmit>("value=Search");
            }
        }

        private static HtmlInputRadioButton GetRarioButtonByValue(string Value)
        {
            return BAT.Browser.Find.ByAttributes<HtmlInputRadioButton>("value=" + Value.ToLower());
        }

        public static ReadOnlyCollection<HtmlAnchor> TopBooks
        {
            get
            {
                return BAT.Browser
                    .Find.ByAttributes<HtmlTableCell>("class=top")
                    .Find.AllByTagName<HtmlAnchor>("a");
            }
        }

        public static void Search(string SearchString, string SearchType) 
        {
            var radioButton = GetRarioButtonByValue(SearchType);
            radioButton.Checked = true;
            SearchBox.Text = SearchString;
            SearchButton.Click();
            BAT.Browser.WaitUntilReady();
        }
    }
}
