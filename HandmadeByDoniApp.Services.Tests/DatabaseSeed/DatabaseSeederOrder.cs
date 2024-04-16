namespace HandmadeByDoniApp.Services.Tests.DatabaseSeed
{
    using HandmadeByDoniApp.Data;
    using HandmadeByDoniApp.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public static class DatabaseSeederOrder
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static UserOrder FirstUserOrder;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static UserOrder SecondUserOrder;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static Order FirstOrder;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static Order SecondOrder;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static ApplicationUser FirstUser;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static ApplicationUser SecondUser;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static Address Address;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public static void SeedDatabaseOrder(HandmadeByDoniAppDbContext dbContext)
        {
          
            // ApplicationUser
            var hasher = new PasswordHasher<ApplicationUser>();

            FirstUser = new ApplicationUser()
            {
                Id = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
                UserName = "Rali@gmail.com",
                NormalizedUserName = "RALI@GMAIL.COM",
                Email = "Rali@gmail.com",
                NormalizedEmail = "RALI@GMAIL.COM",
                EmailConfirmed = false,
                SecurityStamp = ("7HKI4MSRKJKX2DDLAGLVXU7UGKIJVNIR"),
                ConcurrencyStamp = ("cc3f7fbb-f77e-40b3-aeaa-8cc9d75245ea"),
                FirstName = "Ralka",
                LastName = "Slavova"

            };
            FirstUser.PasswordHash = hasher.HashPassword(FirstUser, "user123");

            SecondUser = new ApplicationUser()
            {
                Id = Guid.Parse("371900A3-A5D5-422D-815D-C1D9228C11D0"),
                UserName = "boris@gmail.com",
                NormalizedUserName = "BORIS@GMAIL.COM",
                Email = "boris@gmail.com",
                NormalizedEmail = "BORIS@GMAIL.COM",
                EmailConfirmed = false,
                SecurityStamp = ("37DLJDOBTDVEYX7UIRMCHQ47DPPW5C3I"),
                ConcurrencyStamp = ("8fee1acc-b827-4cb4-a53a-bfbade046f31"),
                FirstName = "Bobi",
                LastName = "Borisov"

            };
            SecondUser.PasswordHash = hasher.HashPassword(SecondUser, "user123");

            dbContext.ApplicationUsers.Add(FirstUser);
            dbContext.ApplicationUsers.Add(SecondUser);

            //Address
            Address = new Address()
            {
                Id = Guid.Parse("E2134209-BFE1-4AD3-8B89-E3C8F95B55C0"),
                CountryName = "Bulgaria",
                CityName = "Varna",
                Street = "137 Slivnitsa Blvd",
                ClientId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
                DeliveryCompanyId = 1,
                MethodPaymentId = 1,
                PhoneNumber = "0898554383"

            };
            dbContext.Addresses.Add(Address);

            //Order
            FirstOrder = new Order()
            {
                Id = Guid.Parse("EE9D71DF-D7E4-4F85-A53E-07BFE35C0208"),
                ClientId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
            };

            SecondOrder = new Order()
            {
                Id = Guid.Parse("F95D5A5C-4B30-4453-8F3B-5BCE14142DCC"),
                ClientId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
            };
            dbContext.Orders.Add(FirstOrder);
            dbContext.Orders.Add(SecondOrder);

            //UserOrder
            FirstUserOrder = new UserOrder()
            {
                UserId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
                OrderId = Guid.Parse("EE9D71DF-D7E4-4F85-A53E-07BFE35C0208"),
                TotalPrice = 65m,
                CreaateOn = DateTime.Parse("2024-04-14 07:36:22.5336115"),
                AddressId = Guid.Parse("E2134209-BFE1-4AD3-8B89-E3C8F95B55C0"),
                IsSent = false,
            };

            SecondUserOrder = new UserOrder()
            {
                UserId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
                OrderId = Guid.Parse("F95D5A5C-4B30-4453-8F3B-5BCE14142DCC"),
                TotalPrice = 50m,
                CreaateOn = DateTime.Parse("2024-04-12 09:06:27.8338791"),
                AddressId = Guid.Parse("E2134209-BFE1-4AD3-8B89-E3C8F95B55C0"),
                IsSent = true,
            };

            dbContext.UsersOrders.Add(FirstUserOrder);
            dbContext.UsersOrders.Add(SecondUserOrder);
            dbContext.SaveChanges();
        }
    }
}
