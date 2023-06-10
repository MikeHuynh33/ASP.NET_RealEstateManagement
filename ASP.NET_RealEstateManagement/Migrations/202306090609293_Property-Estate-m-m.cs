namespace ASP.NET_RealEstateManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyEstatemm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropertyDetailEstateAgents",
                c => new
                    {
                        PropertyDetail_PropertyID = c.Int(nullable: false),
                        EstateAgent_EstateAgentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PropertyDetail_PropertyID, t.EstateAgent_EstateAgentId })
                .ForeignKey("dbo.PropertyDetails", t => t.PropertyDetail_PropertyID, cascadeDelete: true)
                .ForeignKey("dbo.EstateAgents", t => t.EstateAgent_EstateAgentId, cascadeDelete: true)
                .Index(t => t.PropertyDetail_PropertyID)
                .Index(t => t.EstateAgent_EstateAgentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropertyDetailEstateAgents", "EstateAgent_EstateAgentId", "dbo.EstateAgents");
            DropForeignKey("dbo.PropertyDetailEstateAgents", "PropertyDetail_PropertyID", "dbo.PropertyDetails");
            DropIndex("dbo.PropertyDetailEstateAgents", new[] { "EstateAgent_EstateAgentId" });
            DropIndex("dbo.PropertyDetailEstateAgents", new[] { "PropertyDetail_PropertyID" });
            DropTable("dbo.PropertyDetailEstateAgents");
        }
    }
}
