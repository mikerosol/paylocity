using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Objects
{
    public class Employee : Person
    {          
        public List<Objects.Dependent> Dependants { get; set; }
              
        public double GrossPerYear { get; set; }
        public double GrossPerPayCycle { get; set; }

        public Employee()
        {
            PersonType = PersonType.Employee;
            Dependants = new List<Objects.Dependent>();
        }
    }
}