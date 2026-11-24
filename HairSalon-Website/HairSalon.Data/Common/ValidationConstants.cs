namespace HairSalon.Data.Common
{
    public static class ValidationConstants
    {
        public static class HairDresserConstants
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
            public const int AcademicCertificateUrlMaxLength = 200;
            public const int ProfilePictureUrlMaxLength = 200;
            public const int DetailsMaxLength = 250;
            public const int EmailMaxLength = 254;
            public const string EmailPattern = @"^[^@\s]+@(?:[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,63}$";
        }

        public static class ReservationConstants
        {
            public const int ClientNameMinLength = 3;
            public const int ClientNameMaxLength = 50;
            public const int ClientPhoneMinLength = 10;
            public const int ClientPhoneMaxLength = 20;
            public const string ClientPhonePattern = @"^[0-9]+$";
        }

        public static class ServiceConstants
        {
            public const int DurationInMinutesMin = 6;
            public const int DurationInMinutesMax = 480;
            public const decimal PriceMin = 0.01m;
            public const decimal PriceMax = 10000.00m;
            public const int ServicePhotoExampleUrlMaxLength = 100;
            public const int DetailsMaxLength = 2000;
        }
    }
}
