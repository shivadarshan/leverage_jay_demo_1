namespace leverage_jay_demo_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeimage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicle_dm", "Image_Path", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicle_dm", "Image_Path", c => c.String(nullable: false));
        }
    }
}
