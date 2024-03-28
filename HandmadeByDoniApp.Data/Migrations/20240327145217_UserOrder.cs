using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class UserOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Decantres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Boxs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sets_ApplicationUserId",
                table: "Sets",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Glasses_ApplicationUserId",
                table: "Glasses",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Decantres_ApplicationUserId",
                table: "Decantres",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxs_ApplicationUserId",
                table: "Boxs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxs_AspNetUsers_ApplicationUserId",
                table: "Boxs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decantres_AspNetUsers_ApplicationUserId",
                table: "Decantres",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxs_AspNetUsers_ApplicationUserId",
                table: "Boxs");

            migrationBuilder.DropForeignKey(
                name: "FK_Decantres_AspNetUsers_ApplicationUserId",
                table: "Decantres");

            migrationBuilder.DropForeignKey(
                name: "FK_Glasses_AspNetUsers_ApplicationUserId",
                table: "Glasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_AspNetUsers_ApplicationUserId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_ApplicationUserId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Glasses_ApplicationUserId",
                table: "Glasses");

            migrationBuilder.DropIndex(
                name: "IX_Decantres_ApplicationUserId",
                table: "Decantres");

            migrationBuilder.DropIndex(
                name: "IX_Boxs_ApplicationUserId",
                table: "Boxs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Decantres");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Boxs");
        }
    }
}
