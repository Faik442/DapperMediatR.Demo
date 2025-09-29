using FluentMigrator;

namespace DapperMediatR.Demo.API.Repositories.Migrations
{
    [Migration(20240720, "InitialMigration_V1")]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            
            Create.Table("Products")
                .WithColumn("ProductId").AsInt64().PrimaryKey().Identity()
                .WithColumn("ProductName").AsString(255).NotNullable()
                .WithColumn("Price").AsDecimal()
                .WithColumn("CreatedDate").AsDateTime().NotNullable()
                .WithColumn("UpdatedDate").AsDateTime();

            Create.Table("Users")
                .WithColumn("UserId").AsInt64().PrimaryKey().Identity()
                .WithColumn("UserName").AsString(255).NotNullable()
                .WithColumn("CreatedDate").AsDateTime().NotNullable();

            Create.Table("Carts")
                .WithColumn("CartId").AsInt64().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt64().ForeignKey("Users", "UserId") 
                .WithColumn("TotalPrice").AsDecimal().Nullable()
                .WithColumn("CreatedDate").AsDateTime();

            Create.Table("CartProducts")
                .WithColumn("CartProductId").AsInt64().PrimaryKey().Identity()
                .WithColumn("CartId").AsInt64().ForeignKey("Carts", "CartId")
                .WithColumn("ProductId").AsInt32().ForeignKey("Products", "ProductId");

            Create.Index("IX_CartProducts_CartId_ProductId")
                .OnTable("CartProducts")
                .OnColumn("CartId").Ascending()
                .OnColumn("ProductId").Ascending()
                .WithOptions().NonClustered();
        }

        public override void Down()
        {
            Delete.Index("IX_CartProducts_CartId_ProductId").OnTable("CartProducts");

            Delete.ForeignKey("FK_CartProducts_Carts").OnTable("CartProducts");
            Delete.ForeignKey("FK_CartProducts_Products").OnTable("CartProducts");
            Delete.Table("CartProducts");

            Delete.ForeignKey("FK_Carts_Users").OnTable("Carts");
            Delete.Table("Carts");

            Delete.Table("Products");
            Delete.Table("Users");
        }
    }
}
