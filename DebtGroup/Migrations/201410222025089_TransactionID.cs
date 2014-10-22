namespace DebtGroup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionID : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Transactions");
            AddColumn("dbo.Transactions", "TransactionID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Transactions", new[] { "ID", "TransactionID", "Purchaser", "SplitWith" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Transactions");
            DropColumn("dbo.Transactions", "TransactionID");
            AddPrimaryKey("dbo.Transactions", new[] { "ID", "Purchaser", "SplitWith" });
        }
    }
}
