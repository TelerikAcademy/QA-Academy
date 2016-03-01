using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using System.Linq;

namespace ITeBooks
{
    [Binding]
    public class UIStepDefinition
    {
        [Given(@"I'm on IT eBooks home page")]
        public void GivenIMOnITEBooksHomePage()
        {
            Navigator.GoToITeBooksHome();
        }

        [When(@"I seach for (.*) by (.*)")]
        public void WhenISeach(string BookTitle, string SearchType)
        {
            HomePage.Search(BookTitle, SearchType.ToLower());
        }

        [Then(@"result contains (.*) book")]
        public void ThenResultContainsBook(string BookTitle)
        {
            var result = ResultsPage.GetResult(BookTitle);
            Assert.IsNotNull(result, "{0} is not found", BookTitle);
        }

        [Given(@"results page with some results")]
        public void GivenResultsPage()
        {
            Navigator.GoToSearchResultPage("Selenium", "title");
        }

        [When(@"I click on some of the books on results page")]
        public void WhenIClickOnSomeOfTheBooksOnResultsPage()
        {
            // Get second result
            var link = ResultsPage.GetResult(1);

            // Store info about this result in sceanrio context
            var book = new Book();
            book.ID = link.HRef.Replace("book", "").Replace("/", "");
            book.Title = link.Title;
            ScenarioContext.Current.AddOrUpdate("book", book);

            // Click the link
            link.Click();
        }

        [When(@"I click on some of the books on home page")]
        public void WhenIClickOnSomeOfTheBooksOnHomePage()
        {
            // Get second result
            var link = HomePage.TopBooks[1];

            // Store info about this result in sceanrio context
            var book = new Book();
            book.ID = link.HRef.Replace("book", "").Replace("/", "");
            book.Title = link.InnerText;
            ScenarioContext.Current.AddOrUpdate("book", book);

            // Click the link
            link.Click();

            // Wait until browser is ready
            BAT.Browser.WaitUntilReady();

            // Wait until browser contains '/book/' in the url
            BAT.Browser.WaitForUrl("/book/", true, 10000);
        }

        [Then(@"I see book details page for the same book")]
        public void ThenISeeBookDetailsPageForTheSameBook()
        {
            var expectedBook = ScenarioContext.Current["book"] as Book;

            var actualBook = new Book();
            actualBook.ID = DetailsPage.Url.TrimEnd('/').Split('/').Last();

            actualBook.Title = DetailsPage.BookTitle;

            Assert.AreEqual(actualBook.ToString(), expectedBook.ToString(), "Navigation failed.");
        }

        [Then(@"book details page for this book is correct")]
        public void ThenBookDetailsPageForThisBookIsCorrect()
        {
            var expectedBook = ScenarioContext.Current["book"] as BookDetails;
            GivenIMOnITEBooksHomePage();
            WhenISeach(expectedBook.Title, "Title");

            var link = ResultsPage.GetResult(expectedBook.Title);
            link.Click();
            DetailsPage.BookInfo.Wait.ForVisible();
            
            Assert.AreEqual(expectedBook.Title, DetailsPage.BookTitle, "Book title is wrong.");
            Assert.AreEqual(expectedBook.SubTitle, DetailsPage.BookSubTitle, "Book sub title is wrong.");
        }
    }
}
