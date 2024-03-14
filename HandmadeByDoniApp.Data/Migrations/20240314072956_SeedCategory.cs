using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class SeedCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GlassCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Wine glasse" },
                    { 2, "Beer glass" },
                    { 3, "Cognac glass" },
                    { 4, "Whiskey glass" },
                    { 5, "Tea cup" },
                    { 6, "champagne glass" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
