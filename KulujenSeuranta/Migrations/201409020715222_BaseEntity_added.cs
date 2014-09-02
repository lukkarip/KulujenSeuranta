namespace KulujenSeuranta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseEntity_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Payments", "CreatedUser", c => c.String());
            AddColumn("dbo.Payments", "Modified", c => c.DateTime());
            AddColumn("dbo.Payments", "ModifiedUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "ModifiedUser");
            DropColumn("dbo.Payments", "Modified");
            DropColumn("dbo.Payments", "CreatedUser");
            DropColumn("dbo.Payments", "Created");
        }
    }
}
