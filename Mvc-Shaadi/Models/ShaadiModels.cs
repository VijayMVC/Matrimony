using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcShaadi.Models
{
   
    public class Profile
    {

           public virtual int ProfileId{get;set;}         
           public virtual string Email { get; set; }
           public virtual string Password { get; set; }
           [Display(Name = "Name")]
           public virtual string Name { get; set; }
           [Display(Name = "Date of Birth")]
           public virtual DateTime Dob { get; set; }
           public virtual bool Gender { get; set; }
           public virtual int MaritalStatusId { get; set; }
           public virtual MaritalStatus MaritalStatus { get; set; }
           public virtual int StateId { get; set; }   
           public virtual State State { get; set; }        
          
        
        public virtual string ImageURL { get; set; }
         [Display(Name = "Description")]
           public virtual string Description{get;set;}          
          
          
    }
    public class MaritalStatus
    {

        public virtual int MaritalStatusId { get; set; }
        [Display(Name = "Marital status")]
        public virtual string Name { get; set; }
        public virtual List<Profile> Profiles { get; set; }

    }
    public class State
    {
        public virtual int StateId { get; set; }
        [Display(Name = "State")]
        public virtual string Name { get; set; }
        public virtual List<Profile> Profiles { get; set; }
    }

   
     
}