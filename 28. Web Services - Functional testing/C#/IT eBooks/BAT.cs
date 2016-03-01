using ArtOfTest.WebAii.Core;
using ITeBooks;

namespace ITeBooks
{
    public static class BAT
    {
        /// <summary>
        /// Get active browser
        /// </summary>
        public static Browser Browser
        {
            get { return Manager.ActiveBrowser; }
        }

        /// <summary>
        ///  Get active manager
        /// </summary>
        public static Manager Manager
        {
            get { return Hooks.MyManager; }
        }
    }
}
