using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HairSalon_Website.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HairDressers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    YearsOfExperiance = table.Column<int>(type: "int", nullable: true),
                    AcademicCertificateUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HairDressers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServicePhotoExampleUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClientPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReservationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReservationTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    HairDresserId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_HairDressers_HairDresserId",
                        column: x => x.HairDresserId,
                        principalTable: "HairDressers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationService",
                columns: table => new
                {
                    ReservationsId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationService", x => new { x.ReservationsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ReservationService_Reservations_ReservationsId",
                        column: x => x.ReservationsId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "Id", "Details", "Gender", "Name", "Price", "ServicePhotoExampleUrl" },
                values: new object[,]
                {
                    { 1, "Комбинирана услуга с мъжко подстригване и прецизно оформяне на брада.", 0, "Подстригване и оформяне на брада", 20.00m, "/images/services/mens-cut-and-beard.png" },
                    { 2, "Мъжко подстригване, съобразено с формата на лицето и желания стил.", 0, "Мъжко подстригване", 15.00m, "/images/services/mens-haircut.png" },
                    { 3, "Контуриране, скъсяване и оформяне на брада.", 0, "Оформяне на брада", 8.00m, "/images/services/beard-trim.png" },
                    { 4, "Класическо бръснене с професионална грижа за кожата.", 0, "Кралско бръснене", 10.00m, "/images/services/royal-shave.png" },
                    { 5, "Подстригване за момчета до 12 години.", 0, "Детско подстригване до 12 години", 10.00m, "/images/services/kids-haircut.png" },
                    { 6, "Топла кърпа за освежаване и комфорт.", 0, "Топла кърпа", 3.00m, "/images/services/hot-towel.png" },
                    { 7, "Почистване с кола маска на лице, нос и уши.", 0, "Кола маска за лице, нос и уши", 3.00m, "/images/services/face-wax.png" },
                    { 8, "Прецизно оформяне и почистване на вежди.", 0, "Оформяне на вежди", 3.00m, "/images/services/eyebrow-shaping.png" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "ClientName", "ClientPhone", "HairDresserId", "ReservationDate", "ReservationTime", "TotalPrice" },
                values: new object[,]
                {
                    { 1, "Иван", "0888123456", 1, new DateOnly(2026, 10, 20), new TimeOnly(10, 0, 0), 15.00m },
                    { 2, "Петър", "0888123457", 1, new DateOnly(2026, 10, 20), new TimeOnly(10, 50, 0), 8.00m },
                    { 3, "Георги", "0888123458", 1, new DateOnly(2026, 10, 20), new TimeOnly(11, 40, 0), 20.00m },
                    { 4, "Николай", "0888123459", 1, new DateOnly(2026, 10, 20), new TimeOnly(12, 30, 0), 10.00m },
                    { 5, "Димитър", "0888123460", 1, new DateOnly(2026, 10, 20), new TimeOnly(14, 0, 0), 10.00m },
                    { 6, "Алекс", "0888123461", 1, new DateOnly(2026, 10, 20), new TimeOnly(14, 50, 0), 18.00m },
                    { 7, "Борис", "0888123462", 1, new DateOnly(2026, 10, 20), new TimeOnly(15, 40, 0), 11.00m },
                    { 8, "Виктор", "0888123463", 1, new DateOnly(2026, 10, 20), new TimeOnly(16, 30, 0), 23.00m },
                    { 9, "Мартин", "0888123464", 1, new DateOnly(2026, 10, 20), new TimeOnly(17, 20, 0), 15.00m },
                    { 10, "Стефан", "0888123465", 1, new DateOnly(2026, 10, 20), new TimeOnly(18, 10, 0), 20.00m },
                    { 11, "Калин", "0888123466", 1, new DateOnly(2026, 10, 20), new TimeOnly(19, 0, 0), 11.00m },
                    { 12, "Антон", "0888123467", 1, new DateOnly(2026, 10, 20), new TimeOnly(19, 50, 0), 13.00m },
                    { 13, "Стоян", "0888123468", 2, new DateOnly(2026, 10, 20), new TimeOnly(10, 50, 0), 20.00m },
                    { 14, "Любомир", "0888123469", 2, new DateOnly(2026, 10, 20), new TimeOnly(12, 30, 0), 8.00m },
                    { 15, "Васил", "0888123470", 2, new DateOnly(2026, 10, 20), new TimeOnly(16, 30, 0), 18.00m },
                    { 16, "Красимир", "0888123471", 2, new DateOnly(2026, 10, 20), new TimeOnly(19, 0, 0), 13.00m },
                    { 17, "Радослав", "0888123472", 3, new DateOnly(2026, 10, 20), new TimeOnly(10, 0, 0), 10.00m },
                    { 18, "Милен", "0888123473", 3, new DateOnly(2026, 10, 20), new TimeOnly(14, 0, 0), 23.00m },
                    { 19, "Тодор", "0888123474", 3, new DateOnly(2026, 10, 20), new TimeOnly(17, 20, 0), 15.00m },
                    { 20, "Румен", "0888123475", 3, new DateOnly(2026, 10, 20), new TimeOnly(19, 50, 0), 11.00m },
                    { 21, "Кирил", "0888123476", 1, new DateOnly(2026, 10, 21), new TimeOnly(10, 0, 0), 20.00m },
                    { 22, "Павел", "0888123477", 1, new DateOnly(2026, 10, 21), new TimeOnly(11, 40, 0), 10.00m },
                    { 23, "Даниел", "0888123478", 1, new DateOnly(2026, 10, 21), new TimeOnly(14, 50, 0), 18.00m },
                    { 24, "Атанас", "0888123479", 1, new DateOnly(2026, 10, 21), new TimeOnly(18, 10, 0), 10.00m },
                    { 25, "Боян", "0888123480", 2, new DateOnly(2026, 10, 21), new TimeOnly(10, 0, 0), 8.00m },
                    { 26, "Валентин", "0888123481", 2, new DateOnly(2026, 10, 21), new TimeOnly(12, 30, 0), 23.00m },
                    { 27, "Григор", "0888123482", 2, new DateOnly(2026, 10, 21), new TimeOnly(15, 40, 0), 15.00m },
                    { 28, "Емил", "0888123483", 2, new DateOnly(2026, 10, 21), new TimeOnly(19, 50, 0), 10.00m },
                    { 29, "Живко", "0888123484", 3, new DateOnly(2026, 10, 21), new TimeOnly(10, 50, 0), 13.00m },
                    { 30, "Захари", "0888123485", 3, new DateOnly(2026, 10, 21), new TimeOnly(14, 0, 0), 20.00m },
                    { 31, "Илия", "0888123486", 3, new DateOnly(2026, 10, 21), new TimeOnly(16, 30, 0), 11.00m },
                    { 32, "Йордан", "0888123487", 3, new DateOnly(2026, 10, 21), new TimeOnly(19, 0, 0), 15.00m },
                    { 33, "Костадин", "0888123488", 1, new DateOnly(2026, 10, 22), new TimeOnly(11, 40, 0), 23.00m },
                    { 34, "Лъчезар", "0888123489", 1, new DateOnly(2026, 10, 22), new TimeOnly(17, 20, 0), 10.00m },
                    { 35, "Момчил", "0888123490", 2, new DateOnly(2026, 10, 22), new TimeOnly(14, 50, 0), 10.00m },
                    { 36, "Ненчо", "0888123491", 2, new DateOnly(2026, 10, 22), new TimeOnly(18, 10, 0), 11.00m },
                    { 37, "Огнян", "0888123492", 3, new DateOnly(2026, 10, 22), new TimeOnly(10, 0, 0), 21.00m },
                    { 38, "Пламен", "0888123493", 3, new DateOnly(2026, 10, 22), new TimeOnly(19, 50, 0), 20.00m }
                });

            migrationBuilder.InsertData(
                table: "ReservationService",
                columns: new[] { "ReservationsId", "ServicesId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 1 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 2 },
                    { 6, 6 },
                    { 7, 3 },
                    { 7, 7 },
                    { 8, 1 },
                    { 8, 8 },
                    { 9, 2 },
                    { 10, 1 },
                    { 11, 3 },
                    { 11, 6 },
                    { 12, 5 },
                    { 12, 8 },
                    { 13, 1 },
                    { 14, 3 },
                    { 15, 2 },
                    { 15, 7 },
                    { 16, 4 },
                    { 16, 6 },
                    { 17, 5 },
                    { 18, 1 },
                    { 18, 6 },
                    { 19, 2 },
                    { 20, 3 },
                    { 20, 8 },
                    { 21, 1 },
                    { 22, 4 },
                    { 23, 2 },
                    { 23, 6 },
                    { 24, 5 },
                    { 25, 3 },
                    { 26, 1 },
                    { 26, 8 },
                    { 27, 2 },
                    { 28, 4 },
                    { 29, 5 },
                    { 29, 6 },
                    { 30, 1 },
                    { 31, 3 },
                    { 31, 7 },
                    { 32, 2 },
                    { 33, 1 },
                    { 33, 6 },
                    { 34, 4 },
                    { 35, 5 },
                    { 36, 3 },
                    { 36, 8 },
                    { 37, 2 },
                    { 37, 6 },
                    { 37, 7 },
                    { 38, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HairDresserId_ReservationDate_ReservationTime",
                table: "Reservations",
                columns: new[] { "HairDresserId", "ReservationDate", "ReservationTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationService_ServicesId",
                table: "ReservationService",
                column: "ServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationService");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "HairDressers");
        }
    }
}
