using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeByDoniApp.Data.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("371900a3-a5d5-422d-815d-c1d9228c11d0"), 0, "8fee1acc-b827-4cb4-a53a-bfbade046f31", "boris@gmail.com", false, "Bobi", "Borisov", false, null, "BORIS@GMAIL.COM", "BORIS@GMAIL.COM", "AQAAAAEAACcQAAAAEKZ6sw4/iRtjGuILSgoCCnWgRwOrXmoIBosY/1WrpBezctD47K2wjkzzYgUowbvonQ==", null, false, "37DLJDOBTDVEYX7UIRMCHQ47DPPW5C3I", false, "boris@gmail.com" },
                    { new Guid("80255d94-aefe-4c1d-abb6-715604db71b0"), 0, "523d2c69-b025-49e2-b98b-5d6740549418", "admin@handmadebydoni.bg", false, "Admin", "Admin", false, null, "ADMIN@HANDMADEBYDONI.BG", "ADMIN@HANDMADEBYDONI.BG", "AQAAAAEAACcQAAAAEF530VgT32JF4tPNEzX7QWG5Ruu/AQmuYStDKABfBWBTMM+OscN3jG/vjDrLP3ia9Q==", null, false, "ZLCGPEWE3P3BDNVK526PG2IX6B6N6N44", false, "admin@handmadebydoni.bg" },
                    { new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"), 0, "cc3f7fbb-f77e-40b3-aeaa-8cc9d75245ea", "Rali@gmail.com", false, "Ralka", "Slavova", false, null, "RALI@GMAIL.COM", "RALI@GMAIL.COM", "AQAAAAEAACcQAAAAEImfFRhCzLH1vGnXIksHXRZ0hyeD/4dPdaqwqqbMdiSGdBEcYhtyf05mDibjYS6qgw==", null, false, "7HKI4MSRKJKX2DDLAGLVXU7UGKIJVNIR", false, "Rali@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Boxs",
                columns: new[] { "Id", "Capacity", "CreatedOn", "Description", "ImageUrl", "IsActive", "OrderId", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("7e302b06-93b6-493d-8784-4da6eb5c91b8"), 2, new DateTime(2024, 3, 18, 18, 5, 28, 153, DateTimeKind.Unspecified).AddTicks(3333), "Box for 2 glasses", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20220212_211449.jpg", true, null, 20m, "Box" },
                    { new Guid("8dc60af6-5e52-4b64-8e3a-343fb3d425fd"), 6, new DateTime(2024, 3, 17, 7, 28, 1, 313, DateTimeKind.Unspecified).AddTicks(3333), "Box for 6 glasses", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20220212_221050.jpg?t=2024-03-18T10%3A17%3A47.754Z", true, null, 60m, "Box" },
                    { new Guid("9dfe60c9-5292-4dc5-9365-dc0864f40d4b"), 4, new DateTime(2024, 3, 17, 7, 31, 23, 206, DateTimeKind.Unspecified).AddTicks(6667), "Box for 4 glasses", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20220212_214106.jpg?t=2024-03-18T10%3A16%3A38.799Z", true, null, 40m, "Box" }
                });

            migrationBuilder.InsertData(
                table: "Decantres",
                columns: new[] { "Id", "Capacity", "CreatedOn", "Description", "ImageUrl", "IsActive", "IsSet", "OrderId", "Price", "SetId", "Title" },
                values: new object[,]
                {
                    { new Guid("2ccbd57f-25ab-4dbb-9ec0-238839422e87"), 400, new DateTime(2024, 3, 18, 17, 52, 29, 283, DateTimeKind.Unspecified).AddTicks(3333), "The Madonna", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20230724_112332%20(Copy).jpg", true, false, null, 82m, null, "The Madonna" },
                    { new Guid("6679b8ae-c473-4514-b5ab-8608dd5c537d"), 1200, new DateTime(2024, 4, 8, 12, 39, 12, 387, DateTimeKind.Unspecified).AddTicks(7104), "", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_204938%20(Copy).jpg", true, true, null, 0m, null, "Sineva" },
                    { new Guid("c7c5d614-a354-420a-a816-10ff8e3bd391"), 750, new DateTime(2024, 3, 18, 18, 9, 2, 116, DateTimeKind.Unspecified).AddTicks(6667), "The glory of the lion", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20230724_111227%20(Copy).jpg", true, false, null, 57m, null, "The glory of the lion" },
                    { new Guid("e62e9313-837b-4a77-8d9f-2f6b21993284"), 750, new DateTime(2024, 3, 15, 18, 34, 53, 770, DateTimeKind.Unspecified), "Angels with crowns", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20230501_224607%20().jpg", true, false, null, 79m, null, "Angels with crowns" }
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
                table: "GlassCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Wine glass" },
                    { 2, "Beer glass" },
                    { 3, "Cognac glass" },
                    { 4, "Whiskey glass" },
                    { 5, "Tea cup" },
                    { 6, "Champagne glass" }
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
                table: "Sets",
                columns: new[] { "Id", "CreatedOn", "DecanterId", "Description", "ImageUrl", "IsActive", "OrderId", "Price", "Title" },
                values: new object[] { new Guid("f57a2a6c-4ddd-46f9-bd6e-8a3faf801527"), new DateTime(2024, 4, 7, 12, 6, 18, 162, DateTimeKind.Unspecified).AddTicks(8495), null, "Two angels 2 cups", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/Cognac%20(Copy).jpg", true, null, 105m, "Two angels 2 cups" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityName", "ClientId", "CountryName", "DeliveryCompanyId", "MethodPaymentId", "PhoneNumber", "Street" },
                values: new object[] { new Guid("e2134209-bfe1-4ad3-8b89-e3c8f95b55c0"), "Varna", new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"), "Bulgaria", 1, 1, "0898554383", "137 Slivnitsa Blvd" });

            migrationBuilder.InsertData(
                table: "Glasses",
                columns: new[] { "Id", "Capacity", "CreatedOn", "Description", "GlassCategoryId", "ImageUrl", "IsActive", "IsSet", "OrderId", "Price", "SetId", "Title" },
                values: new object[,]
                {
                    { new Guid("1f311aed-e45a-4499-9a99-937503eac6fb"), 300, new DateTime(2024, 3, 18, 18, 3, 29, 246, DateTimeKind.Unspecified).AddTicks(6667), "Firebird 1 cup", 1, "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_205953%20(Copy).jpg", true, false, null, 45m, null, "Firebird 'Жар птица' " },
                    { new Guid("2b07a00d-9963-4074-8785-2ded25faca31"), 500, new DateTime(2024, 3, 18, 10, 55, 1, 846, DateTimeKind.Unspecified).AddTicks(6667), "Beer glass The Queen 1 glass", 2, "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_213051%20(Copy).jpg", true, false, null, 70.00m, null, "The Queen" },
                    { new Guid("2d249ad1-fcf5-431e-9dbe-9e18a101ba8e"), 400, new DateTime(2024, 3, 15, 21, 27, 18, 790, DateTimeKind.Unspecified), "The Madonna and Child 1 cup", 1, "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20231218_162414%20().jpg", true, false, null, 67m, null, "The Madonna and Child' " },
                    { new Guid("71410857-73db-48b1-b1bc-453bce46706c"), 150, new DateTime(2024, 4, 7, 12, 6, 12, 826, DateTimeKind.Unspecified).AddTicks(1188), "", 3, "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20231212_125737%20(Copy).jpg", true, true, null, 0m, new Guid("f57a2a6c-4ddd-46f9-bd6e-8a3faf801527"), "Two angels" },
                    { new Guid("c8198bc1-2a95-460f-a56a-711042a71f19"), 350, new DateTime(2024, 3, 18, 17, 49, 43, 330, DateTimeKind.Unspecified), "Mermaid 1 cup", 1, "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20211204_153938%20(Copy).jpg", true, false, null, 65.00m, null, "Mermaid" },
                    { new Guid("e72293aa-e080-4b0b-b72e-31adc1ecde96"), 150, new DateTime(2024, 4, 7, 12, 6, 12, 827, DateTimeKind.Unspecified).AddTicks(1188), "", 3, "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20231212_125737%20(Copy).jpg", true, true, null, 0m, new Guid("f57a2a6c-4ddd-46f9-bd6e-8a3faf801527"), "Two angels" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientId" },
                values: new object[,]
                {
                    { new Guid("ee9d71df-d7e4-4f85-a53e-07bfe35c0208"), new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb") },
                    { new Guid("f95d5a5c-4b30-4453-8f3b-5bce14142dcc"), new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb") }
                });

            migrationBuilder.InsertData(
                table: "Sets",
                columns: new[] { "Id", "CreatedOn", "DecanterId", "Description", "ImageUrl", "IsActive", "OrderId", "Price", "Title" },
                values: new object[] { new Guid("bfa2762a-617c-4119-8462-d599d9588b61"), new DateTime(2024, 4, 8, 12, 39, 12, 387, DateTimeKind.Unspecified).AddTicks(1453), new Guid("6679b8ae-c473-4514-b5ab-8608dd5c537d"), "Sineva - 1 decanter with 2 glasses", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_205122%20(Copy).jpg", true, null, 120m, "Sineva" });

            migrationBuilder.InsertData(
                table: "Decantres",
                columns: new[] { "Id", "Capacity", "CreatedOn", "Description", "ImageUrl", "IsActive", "IsSet", "OrderId", "Price", "SetId", "Title" },
                values: new object[] { new Guid("4ef3846b-6c81-4b91-a702-c69b399ef550"), 1200, new DateTime(2024, 3, 22, 9, 26, 30, 560, DateTimeKind.Unspecified), "Thrace", "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20230724_113929%20(Copy).jpg", false, false, new Guid("f95d5a5c-4b30-4453-8f3b-5bce14142dcc"), 50m, null, "Thrace" });

            migrationBuilder.InsertData(
                table: "Glasses",
                columns: new[] { "Id", "Capacity", "CreatedOn", "Description", "GlassCategoryId", "ImageUrl", "IsActive", "IsSet", "OrderId", "Price", "SetId", "Title" },
                values: new object[,]
                {
                    { new Guid("62848c82-cf7b-4367-adce-6779103e87f6"), 350, new DateTime(2024, 3, 27, 12, 41, 56, 143, DateTimeKind.Unspecified).AddTicks(3333), "Royal Power 1 cup", 1, "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20211204_153642%20(Copy).jpg", false, false, new Guid("ee9d71df-d7e4-4f85-a53e-07bfe35c0208"), 65.00m, null, "Royal Power" },
                    { new Guid("ad666900-3451-47a4-a8b8-75fce9071005"), 200, new DateTime(2024, 4, 8, 12, 39, 12, 387, DateTimeKind.Unspecified).AddTicks(4015), "", 1, "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_205034%20(Copy).jpg", true, true, null, 0m, new Guid("bfa2762a-617c-4119-8462-d599d9588b61"), "Sineva" },
                    { new Guid("d16c0e0e-348e-42e3-beef-f90fb4fe1216"), 200, new DateTime(2024, 4, 8, 12, 39, 12, 387, DateTimeKind.Unspecified).AddTicks(4830), "", 1, "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20210410_205034%20(Copy).jpg", true, true, null, 0m, new Guid("bfa2762a-617c-4119-8462-d599d9588b61"), "Sineva" }
                });

            migrationBuilder.InsertData(
                table: "UsersOrders",
                columns: new[] { "OrderId", "UserId", "AddressId", "CreaateOn", "TotalPrice" },
                values: new object[] { new Guid("ee9d71df-d7e4-4f85-a53e-07bfe35c0208"), new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"), new Guid("e2134209-bfe1-4ad3-8b89-e3c8f95b55c0"), new DateTime(2024, 4, 14, 7, 36, 22, 533, DateTimeKind.Unspecified).AddTicks(6115), 65m });

            migrationBuilder.InsertData(
                table: "UsersOrders",
                columns: new[] { "OrderId", "UserId", "AddressId", "CreaateOn", "IsSent", "TotalPrice" },
                values: new object[] { new Guid("f95d5a5c-4b30-4453-8f3b-5bce14142dcc"), new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"), new Guid("e2134209-bfe1-4ad3-8b89-e3c8f95b55c0"), new DateTime(2024, 4, 12, 9, 6, 27, 833, DateTimeKind.Unspecified).AddTicks(8791), true, 50m });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "BoxId", "CommentId", "CreatedOn", "DecanterId", "GlassId", "SetId", "Text", "UserId", "UserName" },
                values: new object[] { new Guid("caf75e4f-1cde-485d-a4b6-8dd88906c84a"), null, null, new DateTime(2024, 4, 15, 15, 51, 44, 933, DateTimeKind.Unspecified).AddTicks(9021), null, new Guid("62848c82-cf7b-4367-adce-6779103e87f6"), null, "I am very satisfied great product", new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"), "Ralka Slavova" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "BoxId", "CommentId", "CreatedOn", "DecanterId", "GlassId", "SetId", "Text", "UserId", "UserName" },
                values: new object[] { new Guid("ee9e3a69-fb0c-4f9c-bcd3-875eee0a42c6"), null, null, new DateTime(2024, 4, 15, 16, 3, 9, 916, DateTimeKind.Unspecified).AddTicks(4662), new Guid("4ef3846b-6c81-4b91-a702-c69b399ef550"), null, null, "I can't wait for it to arrive.", new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"), "Ralka Slavova" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "BoxId", "CommentId", "CreatedOn", "DecanterId", "GlassId", "SetId", "Text", "UserId", "UserName" },
                values: new object[] { new Guid("a80db61a-2df1-4bc2-864f-19058d7c1ac4"), null, new Guid("caf75e4f-1cde-485d-a4b6-8dd88906c84a"), new DateTime(2024, 4, 15, 16, 1, 8, 667, DateTimeKind.Unspecified).AddTicks(3314), null, null, null, "I am very happy :)", new Guid("80255d94-aefe-4c1d-abb6-715604db71b0"), "Admin Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("371900a3-a5d5-422d-815d-c1d9228c11d0"));

            migrationBuilder.DeleteData(
                table: "Boxs",
                keyColumn: "Id",
                keyValue: new Guid("7e302b06-93b6-493d-8784-4da6eb5c91b8"));

            migrationBuilder.DeleteData(
                table: "Boxs",
                keyColumn: "Id",
                keyValue: new Guid("8dc60af6-5e52-4b64-8e3a-343fb3d425fd"));

            migrationBuilder.DeleteData(
                table: "Boxs",
                keyColumn: "Id",
                keyValue: new Guid("9dfe60c9-5292-4dc5-9365-dc0864f40d4b"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("a80db61a-2df1-4bc2-864f-19058d7c1ac4"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("ee9e3a69-fb0c-4f9c-bcd3-875eee0a42c6"));

            migrationBuilder.DeleteData(
                table: "Decantres",
                keyColumn: "Id",
                keyValue: new Guid("2ccbd57f-25ab-4dbb-9ec0-238839422e87"));

            migrationBuilder.DeleteData(
                table: "Decantres",
                keyColumn: "Id",
                keyValue: new Guid("c7c5d614-a354-420a-a816-10ff8e3bd391"));

            migrationBuilder.DeleteData(
                table: "Decantres",
                keyColumn: "Id",
                keyValue: new Guid("e62e9313-837b-4a77-8d9f-2f6b21993284"));

            migrationBuilder.DeleteData(
                table: "DeliveryCompanies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Glasses",
                keyColumn: "Id",
                keyValue: new Guid("1f311aed-e45a-4499-9a99-937503eac6fb"));

            migrationBuilder.DeleteData(
                table: "Glasses",
                keyColumn: "Id",
                keyValue: new Guid("2b07a00d-9963-4074-8785-2ded25faca31"));

            migrationBuilder.DeleteData(
                table: "Glasses",
                keyColumn: "Id",
                keyValue: new Guid("2d249ad1-fcf5-431e-9dbe-9e18a101ba8e"));

            migrationBuilder.DeleteData(
                table: "Glasses",
                keyColumn: "Id",
                keyValue: new Guid("71410857-73db-48b1-b1bc-453bce46706c"));

            migrationBuilder.DeleteData(
                table: "Glasses",
                keyColumn: "Id",
                keyValue: new Guid("ad666900-3451-47a4-a8b8-75fce9071005"));

            migrationBuilder.DeleteData(
                table: "Glasses",
                keyColumn: "Id",
                keyValue: new Guid("c8198bc1-2a95-460f-a56a-711042a71f19"));

            migrationBuilder.DeleteData(
                table: "Glasses",
                keyColumn: "Id",
                keyValue: new Guid("d16c0e0e-348e-42e3-beef-f90fb4fe1216"));

            migrationBuilder.DeleteData(
                table: "Glasses",
                keyColumn: "Id",
                keyValue: new Guid("e72293aa-e080-4b0b-b72e-31adc1ecde96"));

            migrationBuilder.DeleteData(
                table: "MethodPayments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UsersOrders",
                keyColumns: new[] { "OrderId", "UserId" },
                keyValues: new object[] { new Guid("ee9d71df-d7e4-4f85-a53e-07bfe35c0208"), new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb") });

            migrationBuilder.DeleteData(
                table: "UsersOrders",
                keyColumns: new[] { "OrderId", "UserId" },
                keyValues: new object[] { new Guid("f95d5a5c-4b30-4453-8f3b-5bce14142dcc"), new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb") });

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("e2134209-bfe1-4ad3-8b89-e3c8f95b55c0"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("80255d94-aefe-4c1d-abb6-715604db71b0"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("caf75e4f-1cde-485d-a4b6-8dd88906c84a"));

            migrationBuilder.DeleteData(
                table: "Decantres",
                keyColumn: "Id",
                keyValue: new Guid("4ef3846b-6c81-4b91-a702-c69b399ef550"));

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: new Guid("bfa2762a-617c-4119-8462-d599d9588b61"));

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: new Guid("f57a2a6c-4ddd-46f9-bd6e-8a3faf801527"));

            migrationBuilder.DeleteData(
                table: "Decantres",
                keyColumn: "Id",
                keyValue: new Guid("6679b8ae-c473-4514-b5ab-8608dd5c537d"));

            migrationBuilder.DeleteData(
                table: "DeliveryCompanies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Glasses",
                keyColumn: "Id",
                keyValue: new Guid("62848c82-cf7b-4367-adce-6779103e87f6"));

            migrationBuilder.DeleteData(
                table: "MethodPayments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f95d5a5c-4b30-4453-8f3b-5bce14142dcc"));

            migrationBuilder.DeleteData(
                table: "GlassCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("ee9d71df-d7e4-4f85-a53e-07bfe35c0208"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c5ae3631-31a1-4369-9f2e-8eec685c98eb"));
        }
    }
}
