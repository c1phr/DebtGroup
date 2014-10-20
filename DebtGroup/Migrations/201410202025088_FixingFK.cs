namespace DebtGroup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Person_ID", "dbo.People");
            DropIndex("dbo.Transactions", new[] { "Person_ID" });
            DropColumn("dbo.Transactions", "Purchaser");
            RenameColumn(table: "dbo.Transactions", name: "Person_ID", newName: "Purchaser");
            AlterColumn("dbo.Transactions", "Purchaser", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "Purchaser");
            AddForeignKey("dbo.Transactions", "Purchaser", "dbo.People", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Purchaser", "dbo.People");
            DropIndex("dbo.Transactions", new[] { "Purchaser" });
            AlterColumn("dbo.Transactions", "Purchaser", c => c.Int());
            RenameColumn(table: "dbo.Transactions", name: "Purchaser", newName: "Person_ID");
            AddColumn("dbo.Transactions", "Purchaser", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "Person_ID");
            AddForeignKey("dbo.Transactions", "Person_ID", "dbo.People", "ID");
        }
    }
}
