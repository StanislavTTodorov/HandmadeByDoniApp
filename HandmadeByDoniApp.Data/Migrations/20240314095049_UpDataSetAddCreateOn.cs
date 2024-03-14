using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class UpDataSetAddCreateOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOn",
                table: "Sets",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateOn",
                table: "Sets");
        }
    }
}
