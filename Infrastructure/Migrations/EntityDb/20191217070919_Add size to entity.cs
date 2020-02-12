using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.EntityDb
{
    public partial class Addsizetoentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "BaseEntity",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "BaseEntity");
        }
    }
}
