using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Sets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Glasses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Decantres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Boxs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sets_OrderId",
                table: "Sets",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Glasses_OrderId",
                table: "Glasses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Decantres_OrderId",
                table: "Decantres",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxs_OrderId",
                table: "Boxs",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxs_Orders_OrderId",
                table: "Boxs",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decantres_Orders_OrderId",
                table: "Decantres",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Glasses_Orders_OrderId",
                table: "Glasses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Orders_OrderId",
                table: "Sets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxs_Orders_OrderId",
                table: "Boxs");

            migrationBuilder.DropForeignKey(
                name: "FK_Decantres_Orders_OrderId",
                table: "Decantres");

            migrationBuilder.DropForeignKey(
                name: "FK_Glasses_Orders_OrderId",
                table: "Glasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Orders_OrderId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_OrderId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Glasses_OrderId",
                table: "Glasses");

            migrationBuilder.DropIndex(
                name: "IX_Decantres_OrderId",
                table: "Decantres");

            migrationBuilder.DropIndex(
                name: "IX_Boxs_OrderId",
                table: "Boxs");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Decantres");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Boxs");
        }
    }
}
