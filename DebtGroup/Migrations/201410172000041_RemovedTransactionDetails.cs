namespace DebtGroup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTransactionDetails : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transactions", "PurchaserName");
            DropColumn("dbo.Transactions", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Transactions", "PurchaserName", c => c.String());
        }
    }
}
