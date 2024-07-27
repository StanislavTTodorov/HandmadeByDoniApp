using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class dataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("371900a3-a5d5-422d-815d-c1d9228c11d0"), 0, "8fee1acc-b827-4cb4-a53a-bfbade046f31", "boris@gmail.com", false, "Bobi", "Borisov", false, null, "BORIS@GMAIL.COM", "BORIS@GMAIL.COM", "AQAAAAEAACcQAAAAEL800nBQZUqWxbftoy9X0B+9SfsQGfFu7GKZr6wwvUfhzJLC4T5fk4VCYGusaLmzKA==", null, false, "37DLJDOBTDVEYX7UIRMCHQ47DPPW5C3I", false, "boris@gmail.com" },
                    { new Guid("80255d94-aefe-4c1d-abb6-715604db71b0"), 0, "523d2c69-b025-49e2-b98b-5d6740549418", "admin@handmadebydoni.bg", false, "Admin", "Admin", false, null, "ADMIN@HANDMADEBYDONI.BG", "ADMIN@HANDMADEBYDONI.BG", "AQAAAAEAACcQAAAAELMrwT64nFyLNC88INmnWutNZLQ+Ruttob+MXO8e7v16LivXJqlCw6Cd/YqsXZybOA==", null, false, "ZLCGPEWE3P3BDNVK526PG2IX6B6N6N44", false, "admin@handmadebydoni.bg" },
                    { new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"), 0, "cc3f7fbb-f77e-40b3-aeaa-8cc9d75245ea", "Rali@gmail.com", false, "Ralka", "Slavova", false, null, "RALI@GMAIL.COM", "RALI@GMAIL.COM", "AQAAAAEAACcQAAAAEI/29tUl14LuNDNKs2JSA93S+wtsKC0XcJJI5l8ln94XnAvRlaQtVViY3dLJVxTbxg==", null, false, "7HKI4MSRKJKX2DDLAGLVXU7UGKIJVNIR", false, "Rali@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Wine glass" },
                    { 2, "Beer glass" },
                    { 3, "Cognac glass" },
                    { 4, "Whiskey glass" },
                    { 5, "Tea cup" },
                    { 6, "Champagne glass" },
                    { 7, "Decanter" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryCompanies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Econt" },
                    { 2, "Speedy" }
                });

            migrationBuilder.InsertData(
                table: "MethodPayments",
                columns: new[] { "Id", "Method" },
                values: new object[,]
                {
                    { 1, "Cash payment on delivery" },
                    { 2, "Card payment on delivery" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "OrderId", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("1f311aed-e45a-4499-9a99-937503eac6fb"), 1, new DateTime(2024, 3, 18, 18, 3, 29, 246, DateTimeKind.Unspecified).AddTicks(6667), "Firebird 1 cup", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_205953%20(Copy).jpg", true, null, 45m, "Firebird 'Жар птица' " },
                    { new Guid("2b07a00d-9963-4074-8785-2ded25faca31"), 2, new DateTime(2024, 3, 18, 10, 55, 1, 846, DateTimeKind.Unspecified).AddTicks(6667), "Beer glass The Queen 1 glass", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_213051%20(Copy).jpg", true, null, 70.00m, "The Queen" },
                    { new Guid("2d249ad1-fcf5-431e-9dbe-9e18a101ba8e"), 1, new DateTime(2024, 3, 15, 21, 27, 18, 790, DateTimeKind.Unspecified), "The Madonna and Child 1 cup", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20231218_162414%20().jpg", true, null, 67m, "The Madonna and Child' " },
                    { new Guid("62848c82-cf7b-4367-adce-6779103e87f6"), 1, new DateTime(2024, 3, 27, 12, 41, 56, 143, DateTimeKind.Unspecified).AddTicks(3333), "Royal Power 1 cup", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20211204_153642%20(Copy).jpg", false, null, 65.00m, "Royal Power" },
                    { new Guid("c8198bc1-2a95-460f-a56a-711042a71f19"), 1, new DateTime(2024, 3, 18, 17, 49, 43, 330, DateTimeKind.Unspecified), "Mermaid 1 cup", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20211204_153938%20(Copy).jpg", true, null, 65.00m, "Mermaid" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("371900a3-a5d5-422d-815d-c1d9228c11d0"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("80255d94-aefe-4c1d-abb6-715604db71b0"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DeliveryCompanies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliveryCompanies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MethodPayments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MethodPayments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1f311aed-e45a-4499-9a99-937503eac6fb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2b07a00d-9963-4074-8785-2ded25faca31"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2d249ad1-fcf5-431e-9dbe-9e18a101ba8e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("62848c82-cf7b-4367-adce-6779103e87f6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c8198bc1-2a95-460f-a56a-711042a71f19"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
