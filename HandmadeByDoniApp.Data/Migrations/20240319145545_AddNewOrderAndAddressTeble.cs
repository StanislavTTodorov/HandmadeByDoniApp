using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class AddNewOrderAndAddressTeble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxs_AspNetUsers_ApplicationUserId",
                table: "Boxs");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Decaners_DecanterId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Decaners_AspNetUsers_ApplicationUserId",
                table: "Decaners");

            migrationBuilder.DropForeignKey(
                name: "FK_Glasses_AspNetUsers_ApplicationUserId",
                table: "Glasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_AspNetUsers_ApplicationUserId",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Decaners_DecanterId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_ApplicationUserId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Glasses_ApplicationUserId",
                table: "Glasses");

            migrationBuilder.DropIndex(
                name: "IX_Boxs_ApplicationUserId",
                table: "Boxs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Decaners",
                table: "Decaners");

            migrationBuilder.DropIndex(
                name: "IX_Decaners_ApplicationUserId",
                table: "Decaners");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Boxs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Decaners");

            migrationBuilder.RenameTable(
                name: "Decaners",
                newName: "Decantres");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Decantres",
                table: "Decantres",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersOrders",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreaateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersOrders", x => new { x.UserId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_UsersOrders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrderId",
                table: "AspNetUsers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersOrders_AddressId",
                table: "UsersOrders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersOrders_OrderId",
                table: "UsersOrders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Orders_OrderId",
                table: "AspNetUsers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Decantres_DecanterId",
                table: "Comments",
                column: "DecanterId",
                principalTable: "Decantres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Decantres_DecanterId",
                table: "Sets",
                column: "DecanterId",
                principalTable: "Decantres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Orders_OrderId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Decantres_DecanterId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Decantres_DecanterId",
                table: "Sets");

            migrationBuilder.DropTable(
                name: "UsersOrders");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OrderId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Decantres",
                table: "Decantres");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Decantres",
                newName: "Decaners");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Sets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Glasses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Boxs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Decaners",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Decaners",
                table: "Decaners",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_ApplicationUserId",
                table: "Sets",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Glasses_ApplicationUserId",
                table: "Glasses",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxs_ApplicationUserId",
                table: "Boxs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Decaners_ApplicationUserId",
                table: "Decaners",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxs_AspNetUsers_ApplicationUserId",
                table: "Boxs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Decaners_DecanterId",
                table: "Comments",
                column: "DecanterId",
                principalTable: "Decaners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decaners_AspNetUsers_ApplicationUserId",
                table: "Decaners",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Glasses_AspNetUsers_ApplicationUserId",
                table: "Glasses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_AspNetUsers_ApplicationUserId",
                table: "Sets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Decaners_DecanterId",
                table: "Sets",
                column: "DecanterId",
                principalTable: "Decaners",
                principalColumn: "Id");
        }
    }
}
