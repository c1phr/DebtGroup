namespace DebtGroup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaserID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "Purchaser", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "Purchaser", c => c.String());
        }
    }
}
