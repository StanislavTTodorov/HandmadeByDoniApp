using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class RenoveBoxFromSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Boxs_BoxId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_BoxId",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "BoxId",
                table: "Sets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BoxId",
                table: "Sets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sets_BoxId",
                table: "Sets",
                column: "BoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Boxs_BoxId",
                table: "Sets",
                column: "BoxId",
                principalTable: "Boxs",
                principalColumn: "Id");
        }
    }
}
