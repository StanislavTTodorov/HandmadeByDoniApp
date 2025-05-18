using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class ShipmentNoteNumberInUserOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShipmentNoteNumber",
                table: "UsersOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("371900a3-a5d5-422d-815d-c1d9228c11d0"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELoDKUDpgB0yIMOv1mAbJTVcos1pfMEg5ptznx6kNOr9aIw61+Zhjo1oq708uRc+GA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("80255d94-aefe-4c1d-abb6-715604db71b0"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEFFYOL+3F7H4bdYVcoOh59uKnd9LCgO0sD4AveWjYsGDQO/NfSkzl5sXil5F7EXu3Q==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEIVi5Q8AvGm0kXp1pcmTERcPPWGJcXOvwh6xkREKuXsoDUTXZLir90PU+QFVSfNZ5A==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentNoteNumber",
                table: "UsersOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("371900a3-a5d5-422d-815d-c1d9228c11d0"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEL800nBQZUqWxbftoy9X0B+9SfsQGfFu7GKZr6wwvUfhzJLC4T5fk4VCYGusaLmzKA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("80255d94-aefe-4c1d-abb6-715604db71b0"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELMrwT64nFyLNC88INmnWutNZLQ+Ruttob+MXO8e7v16LivXJqlCw6Cd/YqsXZybOA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEI/29tUl14LuNDNKs2JSA93S+wtsKC0XcJJI5l8ln94XnAvRlaQtVViY3dLJVxTbxg==");
        }
    }
}
