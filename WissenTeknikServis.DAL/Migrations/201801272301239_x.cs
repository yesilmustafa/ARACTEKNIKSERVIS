namespace WissenTeknikServis.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arizalar", "FirmaAdi", c => c.String());
            AlterColumn("dbo.Arizalar", "Aciklama", c => c.String(nullable: false, maxLength: 400));
            DropColumn("dbo.Arizalar", "FaturaKodu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Arizalar", "FaturaKodu", c => c.String());
            AlterColumn("dbo.Arizalar", "Aciklama", c => c.String(nullable: false));
            DropColumn("dbo.Arizalar", "FirmaAdi");
        }
    }
}
