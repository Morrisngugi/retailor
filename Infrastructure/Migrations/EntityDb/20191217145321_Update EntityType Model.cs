using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.EntityDb
{
    public partial class UpdateEntityTypeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_EntityTypeId",
                table: "BaseEntity");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_EntityTypeId",
                table: "BaseEntity",
                column: "EntityTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_EntityTypeId",
                table: "BaseEntity");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_EntityTypeId",
                table: "BaseEntity",
                column: "EntityTypeId",
                unique: true,
                filter: "[EntityTypeId] IS NOT NULL");
        }
    }
}
