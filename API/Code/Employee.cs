using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Code
{
    public class Employee
    {
        public static List<Objects.Employee> Get(int? quoteId = null)
        {
            var employees = quoteId == null ?
                Api.Global.Repository.Employees :
                Api.Global.Repository.Employees.Where(q => q.QuoteId == quoteId).ToList();

            foreach (var employee in employees)
            {
                employee.BenefitCostPerYear = Api.Code.Quote.GetBenefitCostPerYear(employee);
                employee.BenefitCostPerPayCycle = Api.Code.Quote.GetBenefitCostPerPayCycle(employee);
            }

            return employees;
        }
        public static void Save(Objects.Employee employee)
        {
            var existingEmployee = Api.Global.Repository.Employees
                .SingleOrDefault(e => e.PersonId == employee.PersonId);

            if(existingEmployee == null)
            {
                //NEW EMPLOYEE
                if (string.IsNullOrEmpty(employee.FirstName))
                    throw new Exception("First name can not be empty.");

                if (string.IsNullOrEmpty(employee.LastName))
                    throw new Exception("Last name can not be empty.");

                employee.PersonId = employee.PersonId > 0 ? employee.PersonId : Api.Global.Repository.Employees.Count() + 1;
                employee.BenefitCostPerYear = Api.Code.Quote.GetBenefitCostPerYear(employee);
                employee.BenefitCostPerPayCycle = Api.Code.Quote.GetBenefitCostPerPayCycle(employee);
                employee.GrossPerYear = GetGrossPerYear();
                employee.GrossPerPayCycle = GetGrossPerPayCycle();
                Api.Global.Repository.Employees.Add(employee);
            }
            else
            {
                //UPDATE EXISITNG EMPLOYEE
                if (string.IsNullOrEmpty(employee.FirstName))
                    existingEmployee.FirstName = employee.FirstName;

                if (string.IsNullOrEmpty(employee.LastName))
                    existingEmployee.LastName = employee.LastName;
            }
        }
        public static void Delete(int personId)
        {
            Api.Global.Repository.Employees
                .RemoveAll(e => e.PersonId == personId);
        }
             
        public static double GetGrossPerYear()
        {
            return Objects.Constants.EmployeePaycheckAmount * Objects.Constants.AnualPayCycles;
        }
        public static double GetGrossPerPayCycle()
        {
            return Objects.Constants.EmployeePaycheckAmount;
        }
    }
}