using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Code
{
    public class Repository
    {
        #region Singleton
        //MULTI THREADING HANDLING (LOCKING/UNLOCKING)
        private static readonly object mutex = new object();
        private static Repository instance;
        public static Repository Instance()
        {
            if (instance == null)
            {
                //MULTI THREADING HANDLING
                lock (mutex)
                {
                    if (instance == null)
                    {
                        instance = new Repository();
                    }
                }
            }
            return instance;
        }
        #endregion

        public List<Objects.Quote> Quotes;
        public List<Objects.Employee> Employees;
        public List<Objects.Dependent> Dependents;

        private Repository()
        {
            Quotes = new List<Objects.Quote>();
            Employees = new List<Objects.Employee>();
            Dependents = new List<Objects.Dependent>();

            SeedRepository();
        }     

        private void SeedRepository()
        {
            #region QUOTE 1
            Quotes.Add(new Objects.Quote()
            {
                QuoteId = 1
            });
            Employees.Add(new Objects.Employee()
            {
                PersonId = 1,
                QuoteId = 1,
                FirstName = "Alison",
                LastName = "Manning",
                GrossPerYear = Api.Code.Employee.GetGrossPerYear(),
                GrossPerPayCycle = Api.Code.Employee.GetGrossPerPayCycle()
            });
            Dependents.Add(new Objects.Dependent()
            {
                PersonId = 2,
                QuoteId = 1,
                FirstName = "Peter",
                LastName = "Manning",
                RelationshipType = Objects.RelationshipType.Spouse
            });
            Dependents.Add(new Objects.Dependent()
            {
                PersonId = 3,
                QuoteId = 1,
                FirstName = "Damien",
                LastName = "Manning",
                RelationshipType = Objects.RelationshipType.Child
            });
            Dependents.Add(new Objects.Dependent()
            {
                PersonId = 4,
                QuoteId = 1,
                FirstName = "Julian",
                LastName = "Manning",
                RelationshipType = Objects.RelationshipType.Child
            });
            #endregion

            #region QUOTE 2
            Quotes.Add(new Objects.Quote()
            {
                QuoteId = 2
            });
            Employees.Add(new Objects.Employee()
            {
                PersonId = 5,
                QuoteId = 2,
                FirstName = "Blake",
                LastName = "Ace",
                GrossPerYear = Api.Code.Employee.GetGrossPerYear(),
                GrossPerPayCycle = Api.Code.Employee.GetGrossPerPayCycle()
            });
            Dependents.Add(new Objects.Dependent()
            {
                PersonId = 6,
                QuoteId = 2,
                FirstName = "Maria",
                LastName = "Ace",
                RelationshipType = Objects.RelationshipType.Spouse
            });
            Dependents.Add(new Objects.Dependent()
            {
                PersonId = 7,
                QuoteId = 2,
                FirstName = "Caroline",
                LastName = "Ace",
                RelationshipType = Objects.RelationshipType.Child
            });
            #endregion

            #region QUOTE 3
            Quotes.Add(new Objects.Quote()
            {
                QuoteId = 3
            });
            Employees.Add(new Objects.Employee()
            {
                PersonId = 8,
                QuoteId = 3,
                FirstName = "Zack",
                LastName = "Ross",
                GrossPerYear = Api.Code.Employee.GetGrossPerYear(),
                GrossPerPayCycle = Api.Code.Employee.GetGrossPerPayCycle()
            });
            Dependents.Add(new Objects.Dependent()
            {
                PersonId = 9,
                QuoteId = 3,
                FirstName = "Vanessa",
                LastName = "Ross",
                RelationshipType = Objects.RelationshipType.Spouse
            });
            #endregion

            #region QUOTE 4
            Quotes.Add(new Objects.Quote()
            {
                QuoteId = 4
            });
            Employees.Add(new Objects.Employee()
            {
                PersonId = 10,
                QuoteId = 4,
                FirstName = "Jim",
                LastName = "Beans",
                GrossPerYear = Api.Code.Employee.GetGrossPerYear(),
                GrossPerPayCycle = Api.Code.Employee.GetGrossPerPayCycle()
            });
            #endregion
        }
    }
}