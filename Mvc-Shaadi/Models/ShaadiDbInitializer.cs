using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcShaadi.Models
{

    public class ShaadiDbInitializer : DropCreateDatabaseAlways<MvcShaadiContext>
    {
        protected override void Seed(MvcShaadiContext context)
        {
            
       
            context.MaritalStatuses.Add(new MaritalStatus { Name = "Never Married" });

            context.States.Add(new State { Name = "Tamil Nadu" });



            context.Profiles.Add(new Profile
            {
                Email="ziaprog@gmail.com",
                Password="samtron",
                MaritalStatusId = 1,
                State = new State { Name = "Karnataka" },
                Name = "Aliya Sultana",
                Dob=DateTime.Now,
                Description="Aliya Sultana,Assistant Manager,Bangalore.She is a simple,honest,good looking and very religious. ."
            });

          
          
            base.Seed(context);


        }
    }
}