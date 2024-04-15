using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class DbContextv01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryCompanies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliveryCompanies",
                keyColumn: "Id",
                keyValue: 2);

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

            migrationBuilder.DeleteData(
                table: "MethodPayments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MethodPayments",
                keyColumn: "Id",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeliveryCompanies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Econt" },
                    { 2, "Speedy" }
                });

            migrationBuilder.InsertData(
                table: "GlassCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Wine glass" },
                    { 2, "Beer glass" },
                    { 3, "Cognac glass" },
                    { 4, "Whiskey glass" },
                    { 5, "Tea cup" },
                    { 6, "Champagne glass" }
                });

            migrationBuilder.InsertData(
                table: "MethodPayments",
                columns: new[] { "Id", "Method" },
                values: new object[,]
                {
                    { 1, "Cash payment on delivery" },
                    { 2, "Card payment on delivery" }
                });
        }
    }
}
