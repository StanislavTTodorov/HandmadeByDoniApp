using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class EditName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Sets",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Glasses",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Decantres",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Comments",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Boxs",
                newName: "CreatedOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Sets",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Glasses",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Decantres",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Comments",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Boxs",
                newName: "CreateOn");
        }
    }
}
