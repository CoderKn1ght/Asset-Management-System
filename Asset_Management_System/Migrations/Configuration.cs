using Asset_Management_System.Models.AssetManagementSystem;
using System.Collections.Generic;
using System.Linq;
using Asset_Management_System.Data;
using Asset_Management_System.Helper;

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
            var salt = "ASS3TM4N4G3M3NT!";
            var users = new List<User>()
                {
                    new User
                    {
                        UserName = "admin",
                        Password = SecurityHelper.HashPassword("password",ref salt),
                        IsAdmin = true,
                        IsActive = true,
                        Facilities = new List<Facility>()
                    },
                    new User
                    {
                        UserName = "resource_checker",
                        Password = SecurityHelper.HashPassword("password",ref salt),
                        IsAdmin = false,
                        IsActive = true,
                        Facilities = new List<Facility>()
                    }
                };
            users.ForEach(s => context.Users.AddOrUpdate(p => p.UserName, s));
            context.SaveChanges();
            AddOrUpdateUser(context, "resource_checker", "ERB");
            context.SaveChanges();

        }
        void AddOrUpdateUser(AssetManagementContext context, string userName, string facilityName)
        {
            var user = context.Users.SingleOrDefault(c => c.UserName == userName);
            var facility = user.Facilities.SingleOrDefault(i => i.Name == facilityName);
            if (facility == null)
                user.Facilities.Add(context.Facilities.Single(i => i.Name == facilityName));
        }
    }
}
