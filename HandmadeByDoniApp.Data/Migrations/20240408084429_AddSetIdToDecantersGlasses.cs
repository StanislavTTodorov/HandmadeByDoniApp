using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class AddSetIdToDecantersGlasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Glasses_Sets_SetId",
                table: "Glasses");

            migrationBuilder.DropIndex(
                name: "IX_Glasses_SetId",
                table: "Glasses");

            migrationBuilder.AlterColumn<string>(
                name: "SetId",
                table: "Glasses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SetId1",
                table: "Glasses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SetId",
                table: "Decantres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Glasses_SetId1",
                table: "Glasses",
                column: "SetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Glasses_Sets_SetId1",
                table: "Glasses",
                column: "SetId1",
                principalTable: "Sets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Glasses_Sets_SetId1",
                table: "Glasses");

            migrationBuilder.DropIndex(
                name: "IX_Glasses_SetId1",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "SetId1",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "SetId",
                table: "Decantres");

            migrationBuilder.AlterColumn<Guid>(
                name: "SetId",
                table: "Glasses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Glasses_SetId",
                table: "Glasses",
                column: "SetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Glasses_Sets_SetId",
                table: "Glasses",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");
        }
    }
}
