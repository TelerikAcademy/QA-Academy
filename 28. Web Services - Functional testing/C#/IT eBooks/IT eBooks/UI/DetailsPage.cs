using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;

namespace ITeBooks
{
    public static class DetailsPage
    {
        public static string Url
        {
            get
            {
                return BAT.Browser.Url;
            }
        }

        public static HtmlDiv BookInfo
        {
            get
            {
                BAT.Browser.WaitForElement(new HtmlFindExpression("textcontent=Book Details"), 10000, false);
                return BAT.Browser.Find.ByAttributes<HtmlDiv>(@"itemtype=http://schema.org/Book");
            }
        }

        public static string BookTitle
        {
            get
            {
                var titleExpr = new HtmlFindExpression("tagname=h1");
                BAT.Manager
                    .Wait
                    .For<Element>(e => e.InnerText.Length > 1
                    , BookInfo.Find.ByExpression(titleExpr)
                    , 10000);

                return BookInfo.Find.ByExpression(titleExpr).InnerText;
            }
        }

        public static string BookSubTitle
        {
            get
            {
                if (BookInfo.Find.ByExpression(new HtmlFindExpression("tagname=h3")) != null)
                {
                    return BookInfo.Find.ByExpression(new HtmlFindExpression("tagname=h3")).InnerText;
                }
                else 
                {
                    return string.Empty;
                }
            }
        }
    }
}
