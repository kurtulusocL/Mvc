namespace ETicaret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kargoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(),
                        Adres = c.String(),
                        Telefon = c.String(),
                        EMail = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Satis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SatisTarihi = c.DateTime(nullable: false),
                        ToplamTutar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SepetteMi = c.Boolean(nullable: false),
                        KargoTakipNo = c.String(),
                        MusteriId = c.Int(nullable: false),
                        KargoId = c.Int(nullable: false),
                        SiparisDurumId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kargoes", t => t.KargoId, cascadeDelete: true)
                .ForeignKey("dbo.Musteris", t => t.MusteriId, cascadeDelete: true)
                .ForeignKey("dbo.SiparisDurums", t => t.SiparisDurumId, cascadeDelete: true)
                .Index(t => t.MusteriId)
                .Index(t => t.KargoId)
                .Index(t => t.SiparisDurumId);
            
            CreateTable(
                "dbo.Musteris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdiSoyadi = c.String(),
                        EMail = c.String(),
                        TelefonNo = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MusteriAdres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adres = c.String(),
                        AdiSoyadi = c.String(),
                        MusteriId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Musteris", t => t.MusteriId, cascadeDelete: true)
                .Index(t => t.MusteriId);
            
            CreateTable(
                "dbo.SatisDetays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adet = c.Int(nullable: false),
                        Fiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Indirim = c.Single(nullable: false),
                        SatisId = c.Int(nullable: false),
                        UrunId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Satis", t => t.SatisId, cascadeDelete: true)
                .ForeignKey("dbo.Uruns", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.SatisId)
                .Index(t => t.UrunId);
            
            CreateTable(
                "dbo.Uruns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(),
                        Aciklama = c.String(),
                        AlisFiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SatisFiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SonKullanmaTarih = c.DateTime(nullable: false),
                        KategoriId = c.Int(nullable: false),
                        MarkaId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Markas", t => t.MarkaId, cascadeDelete: true)
                .ForeignKey("dbo.Kategoris", t => t.KategoriId, cascadeDelete: true)
                .Index(t => t.KategoriId)
                .Index(t => t.MarkaId);
            
            CreateTable(
                "dbo.Kategoris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(),
                        Aciklama = c.String(),
                        ResimId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resims", t => t.ResimId, cascadeDelete: true)
                .Index(t => t.ResimId);
            
            CreateTable(
                "dbo.OzellikTips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Aciklama = c.String(),
                        KategoriId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategoris", t => t.KategoriId, cascadeDelete: true)
                .Index(t => t.KategoriId);
            
            CreateTable(
                "dbo.OzellikDegers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(),
                        Aciklama = c.String(),
                        OzellikTipId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OzellikTips", t => t.OzellikTipId)
                .Index(t => t.OzellikTipId);
            
            CreateTable(
                "dbo.UrunOzelliks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunId = c.Int(),
                        OzellikTipId = c.Int(nullable: false),
                        OzellikDegerId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OzellikDegers", t => t.OzellikDegerId, cascadeDelete: true)
                .ForeignKey("dbo.OzellikTips", t => t.OzellikTipId, cascadeDelete: true)
                .ForeignKey("dbo.Uruns", t => t.UrunId)
                .Index(t => t.UrunId)
                .Index(t => t.OzellikTipId)
                .Index(t => t.OzellikDegerId);
            
            CreateTable(
                "dbo.Resims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyukYol = c.String(),
                        OrtaYol = c.String(),
                        KucukYol = c.String(),
                        Varsayılan = c.Boolean(nullable: false),
                        SiraNo = c.Int(nullable: false),
                        UrunId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Uruns", t => t.UrunId)
                .Index(t => t.UrunId);
            
            CreateTable(
                "dbo.Markas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(),
                        Aciklama = c.String(),
                        ResimId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resims", t => t.ResimId)
                .Index(t => t.ResimId);
            
            CreateTable(
                "dbo.SiparisDurums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(),
                        Aciklama = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Satis", "SiparisDurumId", "dbo.SiparisDurums");
            DropForeignKey("dbo.SatisDetays", "UrunId", "dbo.Uruns");
            DropForeignKey("dbo.Uruns", "KategoriId", "dbo.Kategoris");
            DropForeignKey("dbo.Resims", "UrunId", "dbo.Uruns");
            DropForeignKey("dbo.Uruns", "MarkaId", "dbo.Markas");
            DropForeignKey("dbo.Markas", "ResimId", "dbo.Resims");
            DropForeignKey("dbo.Kategoris", "ResimId", "dbo.Resims");
            DropForeignKey("dbo.UrunOzelliks", "UrunId", "dbo.Uruns");
            DropForeignKey("dbo.UrunOzelliks", "OzellikTipId", "dbo.OzellikTips");
            DropForeignKey("dbo.UrunOzelliks", "OzellikDegerId", "dbo.OzellikDegers");
            DropForeignKey("dbo.OzellikDegers", "OzellikTipId", "dbo.OzellikTips");
            DropForeignKey("dbo.OzellikTips", "KategoriId", "dbo.Kategoris");
            DropForeignKey("dbo.SatisDetays", "SatisId", "dbo.Satis");
            DropForeignKey("dbo.Satis", "MusteriId", "dbo.Musteris");
            DropForeignKey("dbo.MusteriAdres", "MusteriId", "dbo.Musteris");
            DropForeignKey("dbo.Satis", "KargoId", "dbo.Kargoes");
            DropIndex("dbo.Markas", new[] { "ResimId" });
            DropIndex("dbo.Resims", new[] { "UrunId" });
            DropIndex("dbo.UrunOzelliks", new[] { "OzellikDegerId" });
            DropIndex("dbo.UrunOzelliks", new[] { "OzellikTipId" });
            DropIndex("dbo.UrunOzelliks", new[] { "UrunId" });
            DropIndex("dbo.OzellikDegers", new[] { "OzellikTipId" });
            DropIndex("dbo.OzellikTips", new[] { "KategoriId" });
            DropIndex("dbo.Kategoris", new[] { "ResimId" });
            DropIndex("dbo.Uruns", new[] { "MarkaId" });
            DropIndex("dbo.Uruns", new[] { "KategoriId" });
            DropIndex("dbo.SatisDetays", new[] { "UrunId" });
            DropIndex("dbo.SatisDetays", new[] { "SatisId" });
            DropIndex("dbo.MusteriAdres", new[] { "MusteriId" });
            DropIndex("dbo.Satis", new[] { "SiparisDurumId" });
            DropIndex("dbo.Satis", new[] { "KargoId" });
            DropIndex("dbo.Satis", new[] { "MusteriId" });
            DropTable("dbo.SiparisDurums");
            DropTable("dbo.Markas");
            DropTable("dbo.Resims");
            DropTable("dbo.UrunOzelliks");
            DropTable("dbo.OzellikDegers");
            DropTable("dbo.OzellikTips");
            DropTable("dbo.Kategoris");
            DropTable("dbo.Uruns");
            DropTable("dbo.SatisDetays");
            DropTable("dbo.MusteriAdres");
            DropTable("dbo.Musteris");
            DropTable("dbo.Satis");
            DropTable("dbo.Kargoes");
        }
    }
}
