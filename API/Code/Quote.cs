using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Code
{
    public class Quote
    {
        public static List<Objects.Quote> Get(int? quoteId = null)
        {
            var quotes = quoteId == null ?
                Api.Global.Repository.Quotes :
                Api.Global.Repository.Quotes.Where(q => q.QuoteId == quoteId).ToList();

            foreach (var quote in quotes)
            {
                quote.Employee = Api.Code.Employee.Get(quote.QuoteId).FirstOrDefault();
                quote.Dependents = Api.Code.Dependent.Get(quote.QuoteId);
                quote.TotalBenefitCostPerYear = CalculateTotalBenefitCostPerYear(quote.Employee, quote.Dependents);
                quote.TotalBenefitCostPerPayCycle = CalculateTotalBenefitCostPerPayCycle(quote.TotalBenefitCostPerYear);
            }
            
            return quotes;
        }
        public static Objects.Quote Save(Objects.Quote quote)
        {
            var existingQuote = Api.Global.Repository.Quotes.SingleOrDefault(e => e.QuoteId == quote.QuoteId);

            if (existingQuote == null)
            {
                quote.QuoteId = quote.QuoteId > 0 ? quote.QuoteId : Api.Global.Repository.Quotes.Count() + 1;                
                Api.Global.Repository.Quotes.Add(quote);
            }

            return quote;
        }
        public static void Delete(int quoteId)
        {
            Api.Global.Repository.Quotes.RemoveAll(e => e.QuoteId == quoteId);
        }

        public static double GetBenefitCostPerYear(Objects.Person person)
        {
            double amount = 0;

            switch (person.PersonType)
            {
                case Objects.PersonType.Employee:
                    amount = Objects.Constants.AnnualEmployeeBenefitCost;
                    break;

                case Objects.PersonType.Dependent:
                    amount = Objects.Constants.AnnualDependentBenefitCost;
                    break;

                default:
                    throw new Exception("Unhandled person type.");
            }

            if (FirstLetterDiscount.IsQualified(person))
            {
                amount = FirstLetterDiscount.GetDiscountedAmount(amount);
            }

            return amount;
        }
        public static double GetBenefitCostPerPayCycle(Objects.Person person)
        {
            //VALIDATION
            if (person.BenefitCostPerYear == 0)
            {
                throw new Exception("Benefit cost per year needs to be calculated.");
            }

            return person.BenefitCostPerYear / Objects.Constants.AnualPayCycles;
        }
        private static double CalculateTotalBenefitCostPerYear(Objects.Employee employee, List<Objects.Dependent> dependents)
        {
            double total = 0;

            if (employee != null)
            {
                total += employee.BenefitCostPerYear;
            }
            
            foreach (var dependent in dependents)
            {
                total += dependent.BenefitCostPerYear;
            }

            return total;
        }
        private static double CalculateTotalBenefitCostPerPayCycle(double totalBenefitCostPerYear)
        {
            return totalBenefitCostPerYear / Objects.Constants.AnualPayCycles;
        }
    }
}