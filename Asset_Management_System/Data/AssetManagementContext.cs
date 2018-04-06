using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Asset_Management_System.Models.AssetManagementSystem;

namespace Asset_Management_System.Data
{
    public class AssetManagementContext :DbContext
    {
        public AssetManagementContext() : base("DefaultConnection")
        { }

        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<User> Users { get; set; }
    }
}