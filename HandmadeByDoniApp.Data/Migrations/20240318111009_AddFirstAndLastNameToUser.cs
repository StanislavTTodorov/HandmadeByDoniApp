using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class AddFirstAndLastNameToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Wine glass");

            migrationBuilder.UpdateData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Champagne glass");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Wine glasse");

            migrationBuilder.UpdateData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "champagne glass");
        }
    }
}
