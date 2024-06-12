using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class UpdateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ProductId",
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
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserProduct",
                columns: table => new
                {
                    ApplicationUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserProduct", x => new { x.ApplicationUsersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserProduct_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

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
                name: "IX_ApplicationUserProduct_ProductsId",
                table: "ApplicationUserProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
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
                name: "FK_Comments_Products_ProductId",
                table: "Comments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Comments_Products_ProductId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "ApplicationUserProduct");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ProductId",
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
                name: "ProductId",
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
    }
}
