namespace HandmadeByDoniApp.Services.Tests.DatabaseSeed
{
    using HandmadeByDoniApp.Data;
    using HandmadeByDoniApp.Data.Models;

    public static class DatabaseSeederBox
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static Box FirstBox;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static Box SecondBox;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static void SeedDatabaseBox(HandmadeByDoniAppDbContext dbContext)
        {

            FirstBox = new Box()
            {
                Id = Guid.NewGuid(),
                Title = "Box",
                Description = "Box for 6 glasses",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20220212_221050.jpg?t=2024-03-18T10%3A17%3A47.754Z",
                Capacity = 6,
                Price = 60m,
                CreatedOn = DateTime.Parse("2024-03-17 07:28:01.3133333"),
                IsActive = true

            };

            SecondBox = new Box()
            {
                Id = Guid.NewGuid(),
                Title = "Box",
                Description = "Box for 2 glasses",
                ImageUrl = "https://vxvxeblefmgvrvtnjuha.supabase.co/storage/v1/object/public/image/IMG_20220212_211449.jpg",
                Capacity = 2,
                Price = 20m,
                CreatedOn = DateTime.Parse("2024-03-18 18:05:28.1533333"),
                IsActive = true

            };

            dbContext.Boxs.Add(FirstBox);
            dbContext.Boxs.Add(SecondBox);
            dbContext.SaveChanges();
        }
    }
}
