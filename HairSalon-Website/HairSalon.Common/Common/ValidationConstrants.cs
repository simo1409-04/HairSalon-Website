namespace HairSalon.Common
{
    public static class ValidationConstrants
    {

        public static class HairDreeserConstrants
        {

            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;


        }


        public static class ReservationConstrants
        {

            public const int ClientNameMinLength = 3;
            public const int ClientNameMaxLength = 50;
            public const int ClientPhoneMinLength = 10;
            public const int ClientPhoneMaxLength = 20;


        }

        public static class ServiceConstrants
        {

            public const int DurationInMinutesMin = 6;      // 6 minutes = 0.1 hours
            public const int DurationInMinutesMax = 480;    // 480 minutes = 8 hours
            public const decimal PriceMin = 0.01m;
            public const decimal PriceMax = 10000.00m;
            public const int ServicePhotoExampleUrlMaxLength = 100;
            public const int DetailsMaxLength = 2000;


        }

    }
}
