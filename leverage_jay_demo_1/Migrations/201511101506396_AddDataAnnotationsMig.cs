namespace leverage_jay_demo_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnnotationsMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vehicle_dm",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Registration_Number = c.String(nullable: false),
                        Manufacturer = c.String(nullable: false),
                        Vehicle_Model = c.String(nullable: false),
                        Covered = c.Boolean(nullable: false),
                        Image_Path = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vehicle_dm");
        }
    }
}
