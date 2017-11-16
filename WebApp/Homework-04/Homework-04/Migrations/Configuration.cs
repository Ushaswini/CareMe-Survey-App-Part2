namespace Homework_04.Migrations
{
    using Homework_04.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<Homework_04.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "StudyCoordinator" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var admin = new ApplicationUser
            {
                Email = "admin@gmail.com",
                UserName = "Admin"
               // StudyGroupId = 1 + ""
            };

            manager.Create(admin, "Admin@6010");

            var adminUser = manager.FindByName("Admin");
            manager.AddToRoles(adminUser.Id, "Admin");

            
           /* var studyGroup = new Models.StudyGroup
            {
                StudyGroupId = 1 + "",
                StudyCoordinatorId = adminUser.Id,
                StudyName = "Study Group - 1",
                StudyGroupCreadtedTime = DateTime.Now.ToString()
            };

            context.StudyGroups.AddOrUpdate(studyGroup);*/


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
