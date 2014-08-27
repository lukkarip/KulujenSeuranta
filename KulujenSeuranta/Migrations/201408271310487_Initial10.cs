namespace KulujenSeuranta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "Category");
        }
    }
}
