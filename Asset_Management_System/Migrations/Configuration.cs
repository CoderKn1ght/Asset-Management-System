using Asset_Management_System.Models.AssetManagementSystem;
using System.Collections.Generic;

namespace Asset_Management_System.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Asset_Management_System.Data.AssetManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Asset_Management_System.Data.AssetManagementContext context)
        {
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
            
                var facilities = new List<Facility>()
                {
                    new Facility()
                    {
                        Name = "ERB",
                        Address = "Arlington",
                        Description = "Engineering Research Building",
                        IsActive = true
                    },
                    new Facility()
                    {
                        Name = "Library",
                        Address = "Arlington",
                        Description = "Central Library",
                        IsActive = true,
                    }
                };
                facilities.ForEach(s => context.Facilities.AddOrUpdate(p => p.Name, s));
                context.SaveChanges();
            
                var resources = new List<Resource>()
                {
                    new Resource()
                    {
                        IsActive = true,
                        ResourceName = "Chair",
                        Quantity = 15,
                        Description = "Steel Chair",
                        Color = "Black",
                        Size = "Small",
                        FacilityId = 1
                    },
                    new Resource()
                    {
                        IsActive = true,
                        ResourceName = "Table",
                        Quantity = 10,
                        Description = "Wooden Table",
                        Color = "Black",
                        Size = "Small",
                        FacilityId = 1
                    },
                    new Resource()
                    {
                        IsActive = true,
                        ResourceName = "Chair",
                        Quantity = 20,
                        Description = "Wooden Chair",
                        Color = "Red",
                        Size = "Small",
                        FacilityId = 2
                    }
                };
            resources.ForEach(s => context.Resources.AddOrUpdate(p => p.ResourceName, s));
            context.SaveChanges();

            var users = new List<User>()
                {
                    new User
                    {
                        UserName = "toor",
                        Password = "password",
                        IsAdmin = true
                    },
                    new User
                    {
                        UserName = "shashank",
                        Password = "password",
                        IsAdmin = false
                    }
                };
            users.ForEach(s => context.Users.AddOrUpdate(p => p.UserName, s));
            context.SaveChanges();
            var usersToFacilities = new List<UserToFacility>()
            {
                new UserToFacility()
                {
                    FacilityId = 1,
                    UserId = 1
                },
                new UserToFacility()
                {
                    FacilityId = 2,
                    UserId = 1
                },
                new UserToFacility()
                {
                    FacilityId = 2,
                    UserId = 2
                }
            };

            usersToFacilities.ForEach(s => context.UserToFacilities.AddOrUpdate(p => new {p.FacilityId,p.UserId}, s));
            context.SaveChanges();
        }
    }
}
