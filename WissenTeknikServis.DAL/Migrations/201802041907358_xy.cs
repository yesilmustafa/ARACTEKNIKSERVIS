namespace WissenTeknikServis.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Arizalar", "KategoriID", "dbo.Kategoriler");
            DropIndex("dbo.Arizalar", new[] { "KategoriID" });
            AddColumn("dbo.Arizalar", "lat", c => c.String());
            AddColumn("dbo.Arizalar", "lng", c => c.String());
            AlterColumn("dbo.Arizalar", "Aciklama", c => c.String(maxLength: 400));
            DropColumn("dbo.Arizalar", "KategoriID");
            DropTable("dbo.Kategoriler");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Kategoriler",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Arizalar", "KategoriID", c => c.Int(nullable: false));
            AlterColumn("dbo.Arizalar", "Aciklama", c => c.String(nullable: false, maxLength: 400));
            DropColumn("dbo.Arizalar", "lng");
            DropColumn("dbo.Arizalar", "lat");
            CreateIndex("dbo.Arizalar", "KategoriID");
            AddForeignKey("dbo.Arizalar", "KategoriID", "dbo.Kategoriler", "ID", cascadeDelete: true);
        }
    }
}
