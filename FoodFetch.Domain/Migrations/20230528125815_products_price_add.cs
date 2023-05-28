using Microsoft.EntityFrameworkCore.Migrations;


namespace FoodFetch.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ProductsPriceAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.AlterColumn<string>(
                name: "second_name",
                table: "tbl_users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            _ = migrationBuilder.AddColumn<double>(
                name: "price",
                table: "tbl_products",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(
                name: "price",
                table: "tbl_products");

            _ = migrationBuilder.AlterColumn<string>(
                name: "second_name",
                table: "tbl_users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
