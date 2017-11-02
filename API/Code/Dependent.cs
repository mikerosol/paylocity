using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Code
{
    public class Dependent
    {
        public static List<Objects.Dependent> Get(int? quoteId = null)
        {
            var dependents = quoteId == null ?
                Api.Global.Repository.Dependents :
                Api.Global.Repository.Dependents.Where(q => q.QuoteId == quoteId).ToList();

            foreach (var dependent in dependents)
            {
                dependent.BenefitCostPerYear = Api.Code.Quote.GetBenefitCostPerYear(dependent);
                dependent.BenefitCostPerPayCycle = Api.Code.Quote.GetBenefitCostPerPayCycle(dependent);
            }

            return dependents;
        }
        public static void Save(Objects.Dependent dependant)
        {
            var existingDependent = Api.Global.Repository.Dependents
                .SingleOrDefault(d => d.PersonId == dependant.PersonId);

            if (existingDependent == null)
            {
                //NEW DEPENDENT
                if (string.IsNullOrEmpty(dependant.FirstName))
                    throw new Exception("First name can not be empty.");

                if (string.IsNullOrEmpty(dependant.LastName))
                    throw new Exception("Last name can not be empty.");

                if (dependant.QuoteId == 0)
                    throw new Exception("Employee ID can not be empty.");

                dependant.PersonId = dependant.PersonId > 0 ? dependant.PersonId : Api.Global.Repository.Dependents.Count() + 1;
                dependant.QuoteId = dependant.QuoteId;
                dependant.BenefitCostPerYear = Api.Code.Quote.GetBenefitCostPerYear(dependant);
                dependant.BenefitCostPerPayCycle = Api.Code.Quote.GetBenefitCostPerPayCycle(dependant);
                Api.Global.Repository.Dependents.Add(dependant);
            }
            else
            {
                //UPDATE EXISITNG DEPENDENT
                if(dependant.QuoteId > 0)
                    existingDependent.QuoteId = dependant.QuoteId; 

                if(string.IsNullOrEmpty(dependant.FirstName))
                    existingDependent.FirstName = dependant.FirstName;

                if (string.IsNullOrEmpty(dependant.LastName))
                    existingDependent.LastName = dependant.LastName;
            }
        }
        public static void Delete(int personId)
        {
            Api.Global.Repository.Dependents
                .RemoveAll(e => e.PersonId == personId);
        }
    }
}