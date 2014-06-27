using System.Data.Entity;

namespace MvcShaadi.Models
{
    public class MvcShaadiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Mvc_Flipkart.Models.MvcShaadiContext>());

        public MvcShaadiContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<MaritalStatus> MaritalStatuses { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Person> Persons { get; set; }

      
      
    }
}

