namespace Asset_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(maxLength: 60),
                        Address = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceName = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(),
                        Color = c.String(),
                        Size = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ResourceCheckerComments = c.String(),
                        AdminComments = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        FacilityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facilities", t => t.FacilityId, cascadeDelete: true)
                .Index(t => t.FacilityId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        Password = c.String(),
                        EmailId = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserFacilities",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Facility_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Facility_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Facilities", t => t.Facility_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Facility_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFacilities", "Facility_Id", "dbo.Facilities");
            DropForeignKey("dbo.UserFacilities", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Resources", "FacilityId", "dbo.Facilities");
            DropIndex("dbo.UserFacilities", new[] { "Facility_Id" });
            DropIndex("dbo.UserFacilities", new[] { "User_Id" });
            DropIndex("dbo.Resources", new[] { "FacilityId" });
            DropTable("dbo.UserFacilities");
            DropTable("dbo.Users");
            DropTable("dbo.Resources");
            DropTable("dbo.Facilities");
        }
    }
}
