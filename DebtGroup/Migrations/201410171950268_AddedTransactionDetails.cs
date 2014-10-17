namespace DebtGroup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransactionDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "PurchaserName", c => c.String());
            AddColumn("dbo.Transactions", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "Discriminator");
            DropColumn("dbo.Transactions", "PurchaserName");
        }
    }
}
