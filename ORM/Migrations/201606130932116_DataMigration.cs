namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration : DbMigration
    {
        public override void Up()
        {
           
            //CreateTable(
            //    "dbo.Missions",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            TaskId = c.Int(nullable: false),
            //            Name = c.String(maxLength: 50),
            //            Description = c.String(),
            //            IsDone = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Tasks", t => t.TaskId)
            //    .Index(t => t.TaskId);
            
            //CreateTable(
            //    "dbo.Tasks",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FromUserId = c.Int(nullable: false),
            //            ToUserId = c.Int(nullable: false),
            //            DateCreation = c.DateTime(nullable: false),
            //            IsChecked = c.Boolean(nullable: false),
            //            Name = c.String(maxLength: 50),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Users", t => t.FromUserId)
            //    .ForeignKey("dbo.Users", t => t.ToUserId)
            //    .Index(t => t.FromUserId)
            //    .Index(t => t.ToUserId);
            
            //CreateTable(
            //    "dbo.Users",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Login = c.String(nullable: false, maxLength: 50),
            //            Email = c.String(nullable: false, maxLength: 50),
            //            Password = c.String(nullable: false, maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Roles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.UserRole",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false),
            //            RoleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.UserId, t.RoleId })
            //    .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ToUserId", "dbo.Users");
            DropForeignKey("dbo.Tasks", "FromUserId", "dbo.Users");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Users");
            DropForeignKey("dbo.Missions", "TaskId", "dbo.Tasks");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Tasks", new[] { "ToUserId" });
            DropIndex("dbo.Tasks", new[] { "FromUserId" });
            DropIndex("dbo.Missions", new[] { "TaskId" });
            DropTable("dbo.UserRole");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
            DropTable("dbo.Missions");
        }
    }
}
