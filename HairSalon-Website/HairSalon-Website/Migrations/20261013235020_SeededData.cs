using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HairSalon_Website.Migrations
{
    /// <inheritdoc />
    public partial class SeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HairDressers",
                columns: new[] { "Id", "AcademicCertificateUrl", "AdditionalInformation", "Name", "ProfilePictureUrl", "YearsOfExperiance" },
                values: new object[,]
                {
                    { 1, "/images/aleksandar-dimitrov/certificate.png", "Специализира в мъжко подстригване, оформяне на брада и класически барбер стил.", "Александър Димитров", "/images/aleksandar-dimitrov/profile-picture.png", 7 },
                    { 2, "/images/viktoria-petrova/certificate.png", "Специализира в боядисване, балеаж, кичури и съвременни дамски прически.", "Виктория Петрова", "/images/viktoria-petrova/profile-picture.png", 9 },
                    { 3, "/images/martin-ivanov/certificate.png", "Работи с модерни мъжки и унисекс прически, оформяне и ежедневен стайлинг.", "Мартин Иванов", "/images/martin-ivanov/profile-picture.png", 4 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Details", "DurationInMinutes", "Gender", "Name", "Price", "ServicePhotoExampleUrl" },
                values: new object[,]
                {
                    { 1, "Консултация, измиване, подстригване и оформяне.", 45, 0, "Мъжко подстригване", 20.00m, "/images/services/mens-haircut.png" },
                    { 2, "Оформяне, контуриране и грижа за брадата.", 30, 0, "Оформяне на брада", 12.00m, "/images/services/beard-trim.png" },
                    { 3, "Комбинирана услуга за подстригване и оформяне на брада.", 75, 0, "Мъжко подстригване и брада", 30.00m, "/images/services/mens-cut-and-beard.png" },
                    { 4, "Подстригване според типа коса и професионално оформяне със сешоар.", 60, 1, "Дамско подстригване и сешоар", 45.00m, "/images/services/womens-haircut.png" },
                    { 5, "Боядисване на израснали корени и завършващо оформяне.", 120, 1, "Боядисване на корени", 65.00m, "/images/services/root-coloring.png" },
                    { 6, "Индивидуална техника за естествен светъл преливащ ефект.", 210, 1, "Балеаж", 200.00m, "/images/services/balayage.png" }
                });

            migrationBuilder.InsertData(
                table: "HairDresserService",
                columns: new[] { "HairDressersId", "ServicesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 3, 1 },
                    { 3, 3 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "ClientName", "ClientPhone", "HairDresserId", "ReservationDate" },
                values: new object[,]
                {
                    { 1, "Иван Петров", "0888123456", 1, new DateTime(2026, 10, 20, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Мария Георгиева", "0899123456", 2, new DateTime(2026, 10, 20, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Георги Иванов", "0878123456", 3, new DateTime(2026, 10, 20, 9, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Елена Димитрова", "0887456789", 1, new DateTime(2026, 10, 21, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Николай Стоянов", "0898456789", 2, new DateTime(2026, 10, 21, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Десислава Костова", "0879456789", 3, new DateTime(2026, 10, 21, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Петър Христов", "0887567890", 1, new DateTime(2026, 10, 22, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Анна Василева", "0898567890", 2, new DateTime(2026, 10, 22, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Калин Тодоров", "0879567890", 3, new DateTime(2026, 10, 22, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Ралица Николова", "0887678901", 1, new DateTime(2026, 10, 23, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "Стефан Марков", "0898678901", 2, new DateTime(2026, 10, 23, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Йоана Пенева", "0879678901", 3, new DateTime(2026, 10, 23, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "Борислав Илиев", "0887789012", 1, new DateTime(2026, 10, 24, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "Кристина Ангелова", "0898789012", 2, new DateTime(2026, 10, 24, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "Валентин Попов", "0879789012", 3, new DateTime(2026, 10, 24, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "Теодора Миланова", "0887890123", 1, new DateTime(2026, 10, 26, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "Михаил Колев", "0898890123", 2, new DateTime(2026, 10, 26, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "Симона Донева", "0879890123", 3, new DateTime(2026, 10, 26, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "Александра Кирова", "0888901234", 1, new DateTime(2026, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "Даниел Борисов", "0899901234", 2, new DateTime(2026, 10, 27, 17, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ReservationService",
                columns: new[] { "ReservationsId", "ServicesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 4 },
                    { 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HairDresserService",
                keyColumns: new[] { "HairDressersId", "ServicesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "HairDresserService",
                keyColumns: new[] { "HairDressersId", "ServicesId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "HairDresserService",
                keyColumns: new[] { "HairDressersId", "ServicesId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "HairDresserService",
                keyColumns: new[] { "HairDressersId", "ServicesId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "HairDresserService",
                keyColumns: new[] { "HairDressersId", "ServicesId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "HairDresserService",
                keyColumns: new[] { "HairDressersId", "ServicesId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "HairDresserService",
                keyColumns: new[] { "HairDressersId", "ServicesId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "HairDresserService",
                keyColumns: new[] { "HairDressersId", "ServicesId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "HairDresserService",
                keyColumns: new[] { "HairDressersId", "ServicesId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "ReservationService",
                keyColumns: new[] { "ReservationsId", "ServicesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ReservationService",
                keyColumns: new[] { "ReservationsId", "ServicesId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "ReservationService",
                keyColumns: new[] { "ReservationsId", "ServicesId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "HairDressers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HairDressers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HairDressers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
