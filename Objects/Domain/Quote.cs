using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Objects
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public Objects.Employee Employee { get; set; }
        public List<Objects.Dependent> Dependents { get; set; }

        public double TotalBenefitCostPerYear { get; set; }
        public double TotalBenefitCostPerPayCycle { get; set; }


        public Quote()
        {
            Dependents = new List<Objects.Dependent>();
        }
    }
}