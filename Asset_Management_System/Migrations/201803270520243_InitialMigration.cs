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
                        FacilityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facilities", t => t.FacilityId, cascadeDelete: true)
                .Index(t => t.FacilityId);
            
            CreateTable(
                "dbo.UserToFacilities",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FacilityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FacilityId })
                .ForeignKey("dbo.Facilities", t => t.FacilityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserToFacilities", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserToFacilities", "FacilityId", "dbo.Facilities");
            DropForeignKey("dbo.Resources", "FacilityId", "dbo.Facilities");
            DropIndex("dbo.UserToFacilities", new[] { "FacilityId" });
            DropIndex("dbo.UserToFacilities", new[] { "UserId" });
            DropIndex("dbo.Resources", new[] { "FacilityId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserToFacilities");
            DropTable("dbo.Resources");
            DropTable("dbo.Facilities");
        }
    }
}
