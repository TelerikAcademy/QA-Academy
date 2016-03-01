using System.Collections.Generic;

namespace ITeBooks
{
    public class Book
    {
        public object ID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string isbn { get; set; }

        public override string ToString()
        {
            return string.Format("ID={0};Title={1};SubTitle={2};Description={3};Image={4};isbn={5}"
                , this.ID, this.Title, this.SubTitle, this.Description, this.Image, this.isbn);
        }
    }

    public class SearchResult
    {
        public string Error { get; set; }
        public double Time { get; set; }
        public string Total { get; set; }
        public int Page { get; set; }
        public List<Book> Books { get; set; }
    }

    public class BookDetails
    {
        public string Error { get; set; }
        public double Time { get; set; }
        public long ID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Year { get; set; }
        public string Page { get; set; }
        public string Publisher { get; set; }
        public string Image { get; set; }
        public string Download { get; set; }
    }
}
