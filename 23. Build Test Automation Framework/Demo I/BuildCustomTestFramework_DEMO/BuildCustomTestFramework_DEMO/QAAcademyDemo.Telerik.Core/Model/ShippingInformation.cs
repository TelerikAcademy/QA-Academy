namespace QAAcademyDemo.Telerik.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShippingInformation : PurchaseInformation
    {
        public ShippingInformation(string firstName, string lastName, string city)
            : base(firstName, lastName, city)
        {
        }
    }
}