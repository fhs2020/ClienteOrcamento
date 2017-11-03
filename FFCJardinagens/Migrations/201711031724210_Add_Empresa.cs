namespace FFCJardinagens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Empresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "Empresa", c => c.String());
            CreateIndex("dbo.Orcamentoes", "ClienteID");
            AddForeignKey("dbo.Orcamentoes", "ClienteID", "dbo.Clientes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orcamentoes", "ClienteID", "dbo.Clientes");
            DropIndex("dbo.Orcamentoes", new[] { "ClienteID" });
            DropColumn("dbo.Clientes", "Empresa");
        }
    }
}
