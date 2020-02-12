using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.EntityDb
{
    public partial class AddedEntityTypemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_EntityTypeId",
                table: "BaseEntity");

            migrationBuilder.AlterColumn<Guid>(
                name: "EntityTypeId",
                table: "BaseEntity",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_EntityTypeId",
                table: "BaseEntity",
                column: "EntityTypeId",
                unique: true,
                filter: "[EntityTypeId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_EntityTypeId",
                table: "BaseEntity");

            migrationBuilder.AlterColumn<Guid>(
                name: "EntityTypeId",
                table: "BaseEntity",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_EntityTypeId",
                table: "BaseEntity",
                column: "EntityTypeId",
                unique: true);
        }
    }
}
