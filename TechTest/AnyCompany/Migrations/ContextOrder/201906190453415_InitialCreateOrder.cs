namespace AnyCompany.Migrations.ContextOrder
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        VAT = c.Double(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
