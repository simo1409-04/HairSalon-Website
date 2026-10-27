namespace HairSalon.Common
{
    public static class ValidationConstants
    {
        public static class HairDresserConstants
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }

        public static class ReservationConstants
        {
            public const int ClientNameMinLength = 3;
            public const int ClientNameMaxLength = 50;
            public const int ClientPhoneMinLength = 10;
            public const int ClientPhoneMaxLength = 20;
        }

        public static class ServiceConstants
        {
            public const int DurationInMinutesMin = 6;
            public const int DurationInMinutesMax = 480;
            public const string PriceMin = "0.01";
            public const string PriceMax = "10000.00";
            public const int ServicePhotoExampleUrlMaxLength = 100;
            public const int DetailsMaxLength = 2000;
        }
    }
}
