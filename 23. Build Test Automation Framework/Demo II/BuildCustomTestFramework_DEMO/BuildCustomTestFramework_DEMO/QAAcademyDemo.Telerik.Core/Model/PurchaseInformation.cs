namespace QAAcademyDemo.Telerik.Core.Model
{
    public class PurchaseInformation
    {
        public PurchaseInformation(string firstName, string lastName, string city)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.City = city;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }
    }
}