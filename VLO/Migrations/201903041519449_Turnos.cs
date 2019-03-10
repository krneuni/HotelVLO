namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Turnos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Turnos", "Fecha", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Turnos", "Fecha");
        }
    }
}
