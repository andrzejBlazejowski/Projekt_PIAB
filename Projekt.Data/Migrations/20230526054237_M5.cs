using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class M5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductCategory");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "ProductCategory",
                newName: "LinkTitle");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductCategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "LinkTitle",
                table: "ProductCategory",
                newName: "Link");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
