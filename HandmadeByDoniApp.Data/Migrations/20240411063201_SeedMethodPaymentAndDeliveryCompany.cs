using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class SeedMethodPaymentAndDeliveryCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_DeliveryCompany_DeliveryCompanyId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_MethodPayment_MethodPaymentId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MethodPayment",
                table: "MethodPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryCompany",
                table: "DeliveryCompany");

            migrationBuilder.RenameTable(
                name: "MethodPayment",
                newName: "MethodPayments");

            migrationBuilder.RenameTable(
                name: "DeliveryCompany",
                newName: "DeliveryCompanies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MethodPayments",
                table: "MethodPayments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryCompanies",
                table: "DeliveryCompanies",
                column: "Id");

            migrationBuilder.InsertData(
                table: "DeliveryCompanies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Econt" },
                    { 2, "Speedy" }
                });

            migrationBuilder.InsertData(
                table: "MethodPayments",
                columns: new[] { "Id", "Method" },
                values: new object[,]
                {
                    { 1, "Cash payment on delivery" },
                    { 2, "Card payment on delivery" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_DeliveryCompanies_DeliveryCompanyId",
                table: "Addresses",
                column: "DeliveryCompanyId",
                principalTable: "DeliveryCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_MethodPayments_MethodPaymentId",
                table: "Addresses",
                column: "MethodPaymentId",
                principalTable: "MethodPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_DeliveryCompanies_DeliveryCompanyId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_MethodPayments_MethodPaymentId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MethodPayments",
                table: "MethodPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryCompanies",
                table: "DeliveryCompanies");

            migrationBuilder.DeleteData(
                table: "DeliveryCompanies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliveryCompanies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MethodPayments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MethodPayments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "MethodPayments",
                newName: "MethodPayment");

            migrationBuilder.RenameTable(
                name: "DeliveryCompanies",
                newName: "DeliveryCompany");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MethodPayment",
                table: "MethodPayment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryCompany",
                table: "DeliveryCompany",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_DeliveryCompany_DeliveryCompanyId",
                table: "Addresses",
                column: "DeliveryCompanyId",
                principalTable: "DeliveryCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_MethodPayment_MethodPaymentId",
                table: "Addresses",
                column: "MethodPaymentId",
                principalTable: "MethodPayment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
