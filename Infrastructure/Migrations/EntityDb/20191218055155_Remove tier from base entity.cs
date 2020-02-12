using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.EntityDb
{
    public partial class Removetierfrombaseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseEntity_Tier_TierId",
                table: "BaseEntity");

            migrationBuilder.DropTable(
                name: "Tier");

            migrationBuilder.DropIndex(
                name: "IX_BaseEntity_TierId",
                table: "BaseEntity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tier",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EntityStatus = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tier", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_TierId",
                table: "BaseEntity",
                column: "TierId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseEntity_Tier_TierId",
                table: "BaseEntity",
                column: "TierId",
                principalTable: "Tier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
