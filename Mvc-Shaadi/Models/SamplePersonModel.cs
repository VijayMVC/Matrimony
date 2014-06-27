using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcShaadi.Models
{
    public class Person
    {
        public virtual int id{get;set;}
        public virtual string FirstName{get;set;}
        public virtual string LastName{get;set;}
        public virtual DateTime EnrollmentDate {get;set;}

    }
}