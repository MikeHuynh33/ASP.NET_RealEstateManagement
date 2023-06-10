namespace ASP.NET_RealEstateManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyDetails : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PropertyDetails", "ListingAgentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PropertyDetails", "ListingAgentID", c => c.Int(nullable: false));
        }
    }
}
