namespace VLO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Turnos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AsignacionTurnoes", "Fecha", c => c.String(nullable: false));
            DropColumn("dbo.Turnos", "Fecha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Turnos", "Fecha", c => c.String(nullable: false));
            DropColumn("dbo.AsignacionTurnoes", "Fecha");
        }
    }
}
