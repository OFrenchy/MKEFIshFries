namespace MKEFishFries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFishFryTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PeopleId = c.Int(nullable: false),
                        ParishId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parishes", t => t.ParishId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PeopleId, cascadeDelete: true)
                .Index(t => t.PeopleId)
                .Index(t => t.ParishId);
            
            CreateTable(
                "dbo.Parishes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Street1 = c.String(),
                        Street2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Lat = c.Double(nullable: false),
                        Long = c.Double(nullable: false),
                        WebsiteURL = c.String(),
                        Phone = c.String(),
                        AdminPersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.AdminPersonId, cascadeDelete: false)
                .Index(t => t.AdminPersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BusinessName = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PesonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PesonId, cascadeDelete: true)
                .Index(t => t.PesonId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParishId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        EventName = c.String(),
                        EventDescription = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Price = c.Int(nullable: false),
                        FoodDescription = c.String(),
                        CarryOutOption = c.String(),
                        SponserPersonId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.SponserPersonId)
                .Index(t => t.SponserPersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "SponserPersonId", "dbo.People");
            DropForeignKey("dbo.Donations", "PesonId", "dbo.People");
            DropForeignKey("dbo.ContactLists", "PeopleId", "dbo.People");
            DropForeignKey("dbo.ContactLists", "ParishId", "dbo.Parishes");
            DropForeignKey("dbo.Parishes", "AdminPersonId", "dbo.People");
            DropForeignKey("dbo.People", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "SponserPersonId" });
            DropIndex("dbo.Donations", new[] { "PesonId" });
            DropIndex("dbo.People", new[] { "ApplicationUserId" });
            DropIndex("dbo.Parishes", new[] { "AdminPersonId" });
            DropIndex("dbo.ContactLists", new[] { "ParishId" });
            DropIndex("dbo.ContactLists", new[] { "PeopleId" });
            DropTable("dbo.Events");
            DropTable("dbo.Donations");
            DropTable("dbo.People");
            DropTable("dbo.Parishes");
            DropTable("dbo.ContactLists");
        }
    }
}
