namespace FFCJardinagens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Telefone = c.String(),
                        Celular = c.String(),
                        CNPJ = c.String(),
                        Endereco = c.String(),
                        Cidade = c.String(),
                        UF = c.String(),
                        CEP = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orcamentoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClienteID = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Descriminação = c.String(),
                        ProdutoUnidade = c.Int(nullable: false),
                        ProdutoTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orcamentoes");
            DropTable("dbo.Clientes");
        }
    }
}
