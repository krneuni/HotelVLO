namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstadoPedido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidoes", "Estado", c => c.Int(nullable: false));
            DropColumn("dbo.DetallePedidoes", "Estado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DetallePedidoes", "Estado", c => c.Int(nullable: false));
            DropColumn("dbo.Pedidoes", "Estado");
        }
    }
}
