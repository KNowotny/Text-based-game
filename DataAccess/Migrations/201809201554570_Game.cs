namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Game : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Armors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArmorName = c.String(),
                        ArmorBuff = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Enemies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 8000, unicode: false),
                        Power = c.Int(nullable: false),
                        Health = c.Int(nullable: false),
                        AcctuallHealth = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemPowerBuff = c.Int(nullable: false),
                        ItemArmorBuff = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(),
                        PlayerPower = c.Int(nullable: false),
                        PlayerAcctualHealth = c.Int(nullable: false),
                        PlayerMaxHealth = c.Int(nullable: false),
                        Score = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Flie = c.String(maxLength: 8000, unicode: false),
                        WeaponId = c.Int(nullable: false),
                        ArmorId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Armors", t => t.ArmorId, cascadeDelete: true)
                .ForeignKey("dbo.Weapons", t => t.WeaponId, cascadeDelete: true)
                .Index(t => t.Flie, unique: true)
                .Index(t => t.WeaponId)
                .Index(t => t.ArmorId);
            
            CreateTable(
                "dbo.Weapons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WeaponName = c.String(),
                        WeaponBuff = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "WeaponId", "dbo.Weapons");
            DropForeignKey("dbo.Players", "ArmorId", "dbo.Armors");
            DropIndex("dbo.Players", new[] { "ArmorId" });
            DropIndex("dbo.Players", new[] { "WeaponId" });
            DropIndex("dbo.Players", new[] { "Flie" });
            DropIndex("dbo.Enemies", new[] { "Name" });
            DropTable("dbo.Weapons");
            DropTable("dbo.Players");
            DropTable("dbo.Items");
            DropTable("dbo.Enemies");
            DropTable("dbo.Armors");
        }
    }
}
