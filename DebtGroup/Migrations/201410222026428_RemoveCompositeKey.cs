namespace DebtGroup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCompositeKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Transactions");
            AlterColumn("dbo.Transactions", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Transactions", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Transactions");
            AlterColumn("dbo.Transactions", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Transactions", new[] { "ID", "TransactionID", "Purchaser", "SplitWith" });
        }
    }
}
