namespace WissenTeknikServis.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Arizalar", "Konum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Arizalar", "Konum", c => c.String(nullable: false));
        }
    }
}
