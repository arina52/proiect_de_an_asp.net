namespace culinaryConnect.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removesurname : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Surname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Surname", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
