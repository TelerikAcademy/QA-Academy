using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;
using System.Linq;
using System;

namespace ITeBooks
{
    [Binding]
    public class APIStepDefinition
    {
        private static string ITeBooksUrl = ConfigurationManager.AppSettings["ITeBooksAPI"];

        [Given(@"book exists in IT eBooks")]
        public void GivenBookExistsInITEBooks()
        {
            WhenGetBookDetailsForId("2279690981");
            var response = ScenarioContext.Current["response"] as HttpResponseMessage;
            var content = response.Content.ReadAsStringAsync().Result;
            var details = JsonConvert.DeserializeObject<BookDetails>(content);
            ScenarioContext.Current.AddOrUpdate<BookDetails>("book", details);
        }

        [When(@"Search with (.*) query")]
        public void WhenSearchWithQuery(string Query)
        {
            var url = string.Format("{0}/search/{1}", ITeBooksUrl, Query);
            var response = HttpHelper.Get(url);
            ScenarioContext.Current.AddOrUpdate("response", response);
        }

        [When(@"Search with (.*) query and page number (.*)")]
        public void WhenSearchWithMysqlQueryAndPageNumber(string Query, string Page)
        {
            var url = string.Format("{0}/search/{1}/page/{2}", ITeBooksUrl, Query, Page);
            var response = HttpHelper.Get(url);
            ScenarioContext.Current.AddOrUpdate("response", response);
        }

        [When(@"get book details for (.*) id")]
        public void WhenGetBookDetailsForId(string Id)
        {
            var url = string.Format("{0}/book/{1}", ITeBooksUrl, Id);
            var response = HttpHelper.Get(url);
            ScenarioContext.Current.AddOrUpdate("response", response);
        }

        [Then(@"status code is (.*)")]
        public void ThenStatusCodeIs(int StatusCode)
        {
            var response = ScenarioContext.Current["response"] as HttpResponseMessage;
            Assert.AreEqual(StatusCode, (int)response.StatusCode, "Status code of the response is wrong.");
        }

        [Then(@"response contains ""(.*)"" error")]
        public void ThenResponseContainsError(string ErrorText)
        {
            var response = ScenarioContext.Current["response"] as HttpResponseMessage;
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<SearchResult>(content);
            Assert.AreEqual(ErrorText, result.Error, "Error is not correct.");
        }

        [Then(@"result contains following books")]
        public void ThenResultContainsFollowingBooks(Table table)
        {
            var response = ScenarioContext.Current["response"] as HttpResponseMessage;
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<SearchResult>(content);

            foreach (var row in table.Rows) 
            {
                var expectedID = row["ID"];
                var expectedTitle = row["Title"];

                var books = result.Books;
                var filteredResult = books
                    .Where<Book>(b => b.ID.ToString() == expectedID)
                    .Where<Book>(b => b.Title == expectedTitle);

                Assert.IsTrue(filteredResult.Count() == 1, "Book with {0} id and {1} title is not found in the results.", expectedID, expectedTitle);
            }
        }

        [Then(@"no results are found")]
        public void ThenNoResultsAreFound()
        {
            var response = ScenarioContext.Current["response"] as HttpResponseMessage;
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<SearchResult>(content);
            Assert.AreEqual("0", result.Total, "{0} results are found.", result.Total);
        }

        [Then(@"result is book object with folllowing details")]
        public void ThenResultIsBookObjectWithFolllowingDetails(Table table)
        {
            var response = ScenarioContext.Current["response"] as HttpResponseMessage;
            var content = response.Content.ReadAsStringAsync().Result;
            var actualResult = JsonConvert.DeserializeObject<BookDetails>(content);

            var expectedID = Convert.ToInt64(table.Rows.Where<TableRow>(r => r["Property"] == "ID").First()["Value"]);
            var expectedTitle = table.Rows.Where<TableRow>(r => r["Property"] == "Title").First()["Value"];
            var expectedAuthor = table.Rows.Where<TableRow>(r => r["Property"] == "Author").First()["Value"];
            var expectedISBN = table.Rows.Where<TableRow>(r => r["Property"] == "ISBN").First()["Value"];
            var expectedYear = table.Rows.Where<TableRow>(r => r["Property"] == "Year").First()["Value"];
            var expectedPublisher = table.Rows.Where<TableRow>(r => r["Property"] == "Publisher").First()["Value"];

            Assert.AreEqual(expectedID, actualResult.ID);
            Assert.AreEqual(expectedTitle, actualResult.Title);
            Assert.AreEqual(expectedAuthor, actualResult.Author);
            Assert.AreEqual(expectedISBN, actualResult.ISBN);
            Assert.AreEqual(expectedYear, actualResult.Year);
            Assert.AreEqual(expectedPublisher, actualResult.Publisher);
        }
    }
}
