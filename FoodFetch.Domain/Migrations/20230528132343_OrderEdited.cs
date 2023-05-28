using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFetch.Domain.Migrations
{
    /// <inheritdoc />
    public partial class OrderEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "delivery_place",
                table: "tbl_orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "request",
                table: "tbl_orders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delivery_place",
                table: "tbl_orders");

            migrationBuilder.DropColumn(
                name: "request",
                table: "tbl_orders");
        }
    }
}
