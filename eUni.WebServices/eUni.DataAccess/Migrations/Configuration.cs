using eUni.DataAccess.eUniDbContext;

namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<eUni.DataAccess.eUniDbContext.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(eUni.DataAccess.eUniDbContext.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var mySeed = new EUniSeed(context);
            mySeed.SeedUsers();
            mySeed.SeedCourses();
          
        }
    }
}
