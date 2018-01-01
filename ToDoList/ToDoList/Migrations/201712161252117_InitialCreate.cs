namespace ToDoList.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Label = c.String(),
                        Created_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ToDo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 200),
                        Start_at = c.DateTime(nullable: false),
                        End_at = c.DateTime(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        User_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 200),
                        Created_at = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Status = c.Int(nullable: false),
                        Role_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.Role_id, cascadeDelete: true)
                .Index(t => t.Role_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDo", "User_id", "dbo.User");
            DropForeignKey("dbo.User", "Role_id", "dbo.Role");
            DropIndex("dbo.User", new[] { "Role_id" });
            DropIndex("dbo.ToDo", new[] { "User_id" });
            DropTable("dbo.User");
            DropTable("dbo.ToDo");
            DropTable("dbo.Role");
        }
    }
}
