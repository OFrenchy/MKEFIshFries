<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 29f0de7fc81ba70e99e132f96bac4d79ef8e5355
namespace MKEFishFries.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MKEFishFries.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
<<<<<<< HEAD
            ContextKey = "MKEFishFries.Models.ApplicationDbContext";
=======
>>>>>>> 29f0de7fc81ba70e99e132f96bac4d79ef8e5355
        }

        protected override void Seed(MKEFishFries.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
<<<<<<< HEAD
=======
=======
namespace MKEFishFries.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MKEFishFries.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MKEFishFries.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
>>>>>>> bf96a3cd0eea6f93475a7820a465571b8a2a4306
>>>>>>> 29f0de7fc81ba70e99e132f96bac4d79ef8e5355
