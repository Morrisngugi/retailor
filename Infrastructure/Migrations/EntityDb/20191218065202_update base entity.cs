using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.EntityDb
{
    public partial class updatebaseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "BaseEntity",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContactPersonId",
                table: "BaseEntity",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SettingId",
                table: "BaseEntity",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "ContactPersonId",
                table: "BaseEntity");

            migrationBuilder.DropColumn(
                name: "SettingId",
                table: "BaseEntity");
        }
    }
}
