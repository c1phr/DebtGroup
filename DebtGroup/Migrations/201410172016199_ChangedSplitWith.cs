namespace DebtGroup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedSplitWith : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "SplitWith", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "SplitWith");
        }
    }
}
