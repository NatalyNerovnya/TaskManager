namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _DataMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Missions", "Description", c => c.String());
            AlterColumn("dbo.Tasks", "Name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Missions", "Description", c => c.String(nullable: false));
        }
    }
}
