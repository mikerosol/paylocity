using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Objects
{
    public enum PersonType { Employee, Dependent } 
    public class Person
    {
        public int PersonId { get; set; }
        public int QuoteId { get; set; }
        public PersonType PersonType { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double BenefitCostPerYear { get; set; }
        public double BenefitCostPerPayCycle { get; set; }        
    }
}