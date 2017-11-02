using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Code
{
    public class FirstLetterDiscount
    {
        public static bool IsQualified(Objects.Person person)
        {
            return person.FirstName.StartsWith("A", true, null) ||
                   person.LastName.StartsWith("A", true, null);
        }        
        public static double GetDiscountedAmount(double amount)
        {
            return amount * (1 - Objects.Constants.FirstLetterDiscountPercent);
        }        
    }
}