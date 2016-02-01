namespace QAAcademyDemo.Telerik.Core.Model
{
    public class BillingInformation : PurchaseInformation
    {
        public BillingInformation(string firstName, string lastName, string city)
            : base(firstName, lastName, city)
        {
        }
    }
}