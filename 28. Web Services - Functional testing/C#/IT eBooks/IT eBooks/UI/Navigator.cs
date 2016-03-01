using System.Configuration;

namespace ITeBooks
{
    public static class Navigator
    {
        private static string ITeBooksUrl = ConfigurationManager.AppSettings["ITeBooksWeb"];

        /// <summary>
        /// Navigates to rul and wait until browser is ready
        /// </summary>
        /// <param name="url">Url</param>
        public static void NavigateTo(string url)
        {
            BAT.Browser.WaitUntilReady();
            BAT.Browser.NavigateTo(url);
            BAT.Browser.WaitUntilReady();
            BAT.Browser.RefreshDomTree();
        }

        /// <summary>
        /// Navigates to home page of ITeBooks web site
        /// </summary>
        public static void GoToITeBooksHome() 
        {
            NavigateTo(ITeBooksUrl);
        }

        /// <summary>
        /// Navigates to details page of gievn book
        /// </summary>
        public static void GoToBookId(string BookId)
        {
            var url = string.Format(@"{0}/book/{1}/", ITeBooksUrl, BookId);
            NavigateTo(url);
        }
        /// <summary>
        /// Navigates to page with results for specified search string.
        /// </summary>
        /// <param name="SearchString"></param>
        /// <param name="SearchType"></param>
        public static void GoToSearchResultPage(string SearchString, string SearchType)
        {
            var url = string.Format(@"{0}/search/?q={1}&type={2}", ITeBooksUrl, SearchString, SearchType);
            NavigateTo(url);
        }
    }
}
