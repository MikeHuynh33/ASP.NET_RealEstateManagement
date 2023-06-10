namespace ASP.NET_RealEstateManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstateAgents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstateAgents",
                c => new
                    {
                        EstateAgentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.EstateAgentId);
            
            CreateTable(
                "dbo.PropertyDetails",
                c => new
                    {
                        PropertyID = c.Int(nullable: false, identity: true),
                        PropertyType = c.String(),
                        PropertyAddress = c.String(),
                        PropertySize = c.String(),
                        NumberOfBedrooms = c.Int(nullable: false),
                        NumberOfBathrooms = c.Int(nullable: false),
                        Amenities = c.String(),
                        PropertyPrice = c.Double(nullable: false),
                        ListingAgentID = c.Int(nullable: false),
                        PropertyDescription = c.String(),
                        PropertyStatus = c.String(),
                        ListingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PropertyDetails");
            DropTable("dbo.EstateAgents");
        }
    }
}
