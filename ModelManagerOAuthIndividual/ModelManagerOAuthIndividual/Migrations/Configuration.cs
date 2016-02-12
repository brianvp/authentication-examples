namespace ModelManagerOAuthIndividual.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using ModelManagerOAuthIndividual.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ModelManagerOAuthIndividual.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ModelManagerOAuthIndividual.Models.ApplicationDbContext";
        }

        protected override void Seed(ModelManagerOAuthIndividual.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var advancedRole = rm.Create(new IdentityRole("ModelEditorRole"));

            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        }
    }
}
