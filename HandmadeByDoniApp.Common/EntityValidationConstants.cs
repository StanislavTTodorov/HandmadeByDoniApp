

namespace HandmadeByDoniApp.Common
{
    public static class EntityValidationConstants
    {
        public static class Product
        {
            public const int TitleMaxLength = 200;
            public const int TitleMinLength = 3;

            public const int DescriptionMaxLength = 2048;

            public const string PriceMaxLength = "7000";
            public const string PriceMinLength = "0";
        }
        public static class Categoty
        {
            public const int NameMaxLength = 100;
            public const int NameMinLength = 2;
        }

        public static class GlassCategoty
        {
            public const int NameMaxLength = 100;
            public const int NameMinLength = 2;
        }

        public static class Glass
        {
            public const int TitleMaxLength = 200;
            public const int TitleMinLength = 3;

            public const int DescriptionMaxLength = 2048;

            public const int ImageUrlMaxLength = 2048;

            public const string PriceMaxLength = "7000";
            public const string PriceMinLength = "0";

            public const string CapacityMaxLength = "2000";
            public const string CapacityMinLength = "50";
        }

        public static class Decanter
        {
            public const int TitleMaxLength = 200;
            public const int TitleMinLength = 3;

            public const int DescriptionMaxLength = 2048;

            public const int ImageUrlMaxLength = 2048;

            public const string PriceMaxLength = "7000";
            public const string PriceMinLength = "0";

            public const string CapacityMaxLength = "2000";
            public const string CapacityMinLength = "200";
        }

        public static class Set
        {
            public const int TitleMaxLength = 200;
            public const int TitleMinLength = 3;

            public const int DescriptionMaxLength = 2048;

            public const string NumberOfCupsMax = "10";
            public const string NumberOfCupsMin = "1";

            public const string NumberOfDecanterMax = "1";
            public const string NumberOfDecanterMin = "0";


            public const int ImageUrlMaxLength = 2048;

            public const string PriceMaxLength = "7000";
            public const string PriceMinLength = "0";
        }

        public static class Box
        {
            public const int TitleMaxLength = 200;
            public const int TitleMinLength = 3;

            public const int DescriptionMaxLength = 2048;

            public const int ImageUrlMaxLength = 2048;

            public const int CapacityMax = 6;
            public const int CapacityMin = 1;
            public const string PriceMaxLength = "1000";
            public const string PriceMinLength = "0";

        }

        public static class Comment 
        { 
            public const int TextMaxLength = 500;
            public const int TextMinLength = 1;
        }

        public static class User
        {
            public const int FirstNameMaxLength = 100;
            public const int FirstNameMinLength = 1;

            public const int LastNameMaxLength =100;
            public const int LastNameMinLength = 1;

            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }

        public static class Address
        {
            public const int CountryNameMaxLength = 60;
            public const int CountryNameMinLength = 3;

            public const int CityNameMaxLength = 90;
            public const int CityNameMinLength = 3;

            public const int StreetMaxLength = 90;
            public const int StreetMinLength = 1;


            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;

        }
        public static class DeliveryCompany
        {
            public const int NameMaxLength = 60;
            public const int NameMinLength = 3;
        }
        public static class MethodPayment
        {
            public const int MethodMaxLength = 60;
            public const int MethodMinLength = 3;
        }
    }
}
