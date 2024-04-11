using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class addIColectionFromUserInProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ApplicationUserBox",
                columns: table => new
                {
                    ApplicationUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoxsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserBox", x => new { x.ApplicationUsersId, x.BoxsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserBox_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserBox_Boxs_BoxsId",
                        column: x => x.BoxsId,
                        principalTable: "Boxs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserDecanter",
                columns: table => new
                {
                    ApplicationUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DecantersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserDecanter", x => new { x.ApplicationUsersId, x.DecantersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserDecanter_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserDecanter_Decantres_DecantersId",
                        column: x => x.DecantersId,
                        principalTable: "Decantres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserGlass",
                columns: table => new
                {
                    ApplicationUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GlassesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGlass", x => new { x.ApplicationUsersId, x.GlassesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGlass_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGlass_Glasses_GlassesId",
                        column: x => x.GlassesId,
                        principalTable: "Glasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserSet",
                columns: table => new
                {
                    ApplicationUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SetsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSet", x => new { x.ApplicationUsersId, x.SetsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserSet_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSet_Sets_SetsId",
                        column: x => x.SetsId,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserBox_BoxsId",
                table: "ApplicationUserBox",
                column: "BoxsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDecanter_DecantersId",
                table: "ApplicationUserDecanter",
                column: "DecantersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGlass_GlassesId",
                table: "ApplicationUserGlass",
                column: "GlassesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSet_SetsId",
                table: "ApplicationUserSet",
                column: "SetsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserBox");

            migrationBuilder.DropTable(
                name: "ApplicationUserDecanter");

            migrationBuilder.DropTable(
                name: "ApplicationUserGlass");

            migrationBuilder.DropTable(
                name: "ApplicationUserSet");

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
    }
}
