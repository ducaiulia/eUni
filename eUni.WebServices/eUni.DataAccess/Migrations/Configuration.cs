using eUni.DataAccess.eUniDbContext;

namespace eUni.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<eUni.DataAccess.eUniDbContext.EUniDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "eUni.DataAccess.eUniDbContext.EUniDbContext";
        }

        protected override void Seed(eUni.DataAccess.eUniDbContext.EUniDbContext context)
        {
            var seed = new EUniSeed(context);
            seed.SeedUsers();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
