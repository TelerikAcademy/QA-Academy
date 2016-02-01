namespace QAAcademyDemo.Telerik.Core.Model
{
    public class ShippingInformation : PurchaseInformation
    {
        public ShippingInformation(string firstName, string lastName, string city)
            : base(firstName, lastName, city)
        {
        }
    }
}