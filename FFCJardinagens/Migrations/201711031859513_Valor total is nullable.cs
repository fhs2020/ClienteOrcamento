namespace FFCJardinagens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Valortotalisnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orcamentoes", "ValorTotal", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orcamentoes", "ValorTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
