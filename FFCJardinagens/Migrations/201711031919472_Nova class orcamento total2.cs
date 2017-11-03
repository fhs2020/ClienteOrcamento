namespace FFCJardinagens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Novaclassorcamentototal2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TotalOrcamentoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClienteID = c.Int(nullable: false),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Orcamentoes", "TotalOrcamento_ID", c => c.Int());
            CreateIndex("dbo.Orcamentoes", "TotalOrcamento_ID");
            AddForeignKey("dbo.Orcamentoes", "TotalOrcamento_ID", "dbo.TotalOrcamentoes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orcamentoes", "TotalOrcamento_ID", "dbo.TotalOrcamentoes");
            DropIndex("dbo.Orcamentoes", new[] { "TotalOrcamento_ID" });
            DropColumn("dbo.Orcamentoes", "TotalOrcamento_ID");
            DropTable("dbo.TotalOrcamentoes");
        }
    }
}
