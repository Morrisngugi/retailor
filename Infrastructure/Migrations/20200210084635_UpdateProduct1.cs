using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdateProduct1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PackagingTypeId",
                table: "Products",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_PackagingTypeId",
                table: "Products",
                column: "PackagingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PackagingType_PackagingTypeId",
                table: "Products",
                column: "PackagingTypeId",
                principalTable: "PackagingType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PackagingType_PackagingTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PackagingTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PackagingTypeId",
                table: "Products");
        }
    }
}
