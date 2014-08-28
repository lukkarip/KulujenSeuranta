namespace KulujenSeuranta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Uusin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "Category", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "Category", c => c.String());
        }
    }
}
