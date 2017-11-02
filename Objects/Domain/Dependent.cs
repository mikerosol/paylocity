using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Objects
{
    public enum RelationshipType { Spouse, Child }

    public class Dependent : Objects.Person
    {
        public RelationshipType RelationshipType { get; set; }

        public Dependent()
        {
            PersonType = PersonType.Dependent;
        }
    }
}