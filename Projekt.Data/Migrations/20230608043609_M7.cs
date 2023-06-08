using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class M7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CheckoutItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutItem_ProductId",
                table: "CheckoutItem",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutItem");
        }
    }
}
