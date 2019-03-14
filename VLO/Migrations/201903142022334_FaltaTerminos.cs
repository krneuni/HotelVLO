namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FaltaTerminos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DetallePedidoes", "IdTerminoCarne", "dbo.TerminosCarnes");
            DropIndex("dbo.DetallePedidoes", new[] { "IdTerminoCarne" });
            DropColumn("dbo.DetallePedidoes", "IdTerminoCarne");
            DropTable("dbo.TerminosCarnes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TerminosCarnes",
                c => new
                    {
                        IdTerminoCarne = c.Int(nullable: false, identity: true),
                        Termino = c.String(),
                    })
                .PrimaryKey(t => t.IdTerminoCarne);
            
            AddColumn("dbo.DetallePedidoes", "IdTerminoCarne", c => c.Int(nullable: false));
            CreateIndex("dbo.DetallePedidoes", "IdTerminoCarne");
            AddForeignKey("dbo.DetallePedidoes", "IdTerminoCarne", "dbo.TerminosCarnes", "IdTerminoCarne", cascadeDelete: true);
        }
    }
}
