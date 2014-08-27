namespace KulujenSeuranta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Payments", new[] { "User_Id" });
            AlterColumn("dbo.Payments", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Payments", "User_Id");
            AddForeignKey("dbo.Payments", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Payments", new[] { "User_Id" });
            AlterColumn("dbo.Payments", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Payments", "User_Id");
            AddForeignKey("dbo.Payments", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
