namespace MKEFishFries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPesonToPersonInDonations : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Donations", name: "PesonId", newName: "PersonId");
            RenameIndex(table: "dbo.Donations", name: "IX_PesonId", newName: "IX_PersonId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Donations", name: "IX_PersonId", newName: "IX_PesonId");
            RenameColumn(table: "dbo.Donations", name: "PersonId", newName: "PesonId");
        }
    }
}
