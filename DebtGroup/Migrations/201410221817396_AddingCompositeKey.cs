namespace DebtGroup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCompositeKey : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Transactions", new[] { "Purchaser" });
            //DropColumn("dbo.Transactions", "SplitWith");
            //RenameColumn(table: "dbo.Transactions", name: "Purchaser", newName: "SplitWith");
            DropPrimaryKey("dbo.Transactions");
            AlterColumn("dbo.Transactions", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Transactions", "SplitWith", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Transactions", new[] { "ID", "Purchaser", "SplitWith" });
            CreateIndex("dbo.Transactions", "Purchaser");
            CreateIndex("dbo.Transactions", "SplitWith");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Transactions", new[] { "SplitWith" });
            DropIndex("dbo.Transactions", new[] { "Purchaser" });
            DropPrimaryKey("dbo.Transactions");
            AlterColumn("dbo.Transactions", "SplitWith", c => c.String());
            AlterColumn("dbo.Transactions", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Transactions", "ID");
            RenameColumn(table: "dbo.Transactions", name: "SplitWith", newName: "Purchaser");
            AddColumn("dbo.Transactions", "SplitWith", c => c.String());
            CreateIndex("dbo.Transactions", "Purchaser");
        }
    }
}
