namespace FFCJardinagens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orcamentoes", "DataOrcamento", c => c.DateTime());
            AddColumn("dbo.TotalOrcamentoes", "DataOrcamento", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TotalOrcamentoes", "DataOrcamento");
            DropColumn("dbo.Orcamentoes", "DataOrcamento");
        }
    }
}
