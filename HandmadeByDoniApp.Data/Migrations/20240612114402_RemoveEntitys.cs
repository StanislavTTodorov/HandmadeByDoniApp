using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class RemoveEntitys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Boxs_BoxId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Decantres_DecanterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Glasses_GlassId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sets_SetId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Boxs_BoxId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Decantres_DecanterId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Glasses_GlassId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Sets_SetId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Decantres_Sets_SetId",
                table: "Decantres");

            migrationBuilder.DropTable(
                name: "Boxs");

            migrationBuilder.DropTable(
                name: "Glasses");

            migrationBuilder.DropTable(
                name: "GlassCategories");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "Decantres");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BoxId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_DecanterId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_GlassId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SetId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BoxId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DecanterId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GlassId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SetId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BoxId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DecanterId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "GlassId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SetId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BoxId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DecanterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GlassId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SetId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BoxId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DecanterId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GlassId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SetId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BoxId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DecanterId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GlassId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SetId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Boxs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boxs_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GlassCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlassCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Decantres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSet = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decantres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decantres_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DecanterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sets_Decantres_DecanterId",
                        column: x => x.DecanterId,
                        principalTable: "Decantres",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sets_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Glasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GlassCategoryId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSet = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Glasses_GlassCategories_GlassCategoryId",
                        column: x => x.GlassCategoryId,
                        principalTable: "GlassCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Glasses_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Glasses_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BoxId",
                table: "Comments",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_DecanterId",
                table: "Comments",
                column: "DecanterId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GlassId",
                table: "Comments",
                column: "GlassId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SetId",
                table: "Comments",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BoxId",
                table: "AspNetUsers",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DecanterId",
                table: "AspNetUsers",
                column: "DecanterId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GlassId",
                table: "AspNetUsers",
                column: "GlassId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SetId",
                table: "AspNetUsers",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxs_OrderId",
                table: "Boxs",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Decantres_OrderId",
                table: "Decantres",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Decantres_SetId",
                table: "Decantres",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Glasses_GlassCategoryId",
                table: "Glasses",
                column: "GlassCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Glasses_OrderId",
                table: "Glasses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Glasses_SetId",
                table: "Glasses",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_DecanterId",
                table: "Sets",
                column: "DecanterId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_OrderId",
                table: "Sets",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Boxs_BoxId",
                table: "AspNetUsers",
                column: "BoxId",
                principalTable: "Boxs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Decantres_DecanterId",
                table: "AspNetUsers",
                column: "DecanterId",
                principalTable: "Decantres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Glasses_GlassId",
                table: "AspNetUsers",
                column: "GlassId",
                principalTable: "Glasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sets_SetId",
                table: "AspNetUsers",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Boxs_BoxId",
                table: "Comments",
                column: "BoxId",
                principalTable: "Boxs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Decantres_DecanterId",
                table: "Comments",
                column: "DecanterId",
                principalTable: "Decantres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Glasses_GlassId",
                table: "Comments",
                column: "GlassId",
                principalTable: "Glasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Sets_SetId",
                table: "Comments",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Decantres_Sets_SetId",
                table: "Decantres",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");
        }
    }
}
