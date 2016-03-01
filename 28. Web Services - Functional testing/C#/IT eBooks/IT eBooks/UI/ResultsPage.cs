using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using System.Linq;

namespace ITeBooks
{
    public static class ResultsPage
    {
        public static HtmlTable ResultTable
        {
            get
            {
                var tableExpr = new HtmlFindExpression("xpath=/html/body/table/tbody/tr[2]/td/table");
                BAT.Browser.WaitForElement(tableExpr, 10000, false);
                return BAT.Browser.Find.ByExpression<HtmlTable>(tableExpr);
            }
        }

        public static HtmlAnchor GetResult(string BookTitle) 
        {
            Wait.Until(() =>
            {
                BAT.Browser.RefreshDomTree();
                var result = ResultTable.Find.AllByTagName<HtmlAnchor>("a")
                .Where<HtmlAnchor>(a => a.InnerText == BookTitle)
                .First();
                return result != null;
            }, 10);

            return ResultTable.Find.AllByTagName<HtmlAnchor>("a")
                .Where<HtmlAnchor>(a => a.InnerText == BookTitle)
                .First();
        }

        public static HtmlAnchor GetResult(int Index)
        {
            BAT.Manager
                .Wait
                .For<HtmlTable>(t => t.Find.AllByTagName<HtmlAnchor>("a").Count > Index
                , ResultTable, 10000);

            return ResultTable.Find.AllByTagName<HtmlAnchor>("a")[Index];
        }
    }
}
