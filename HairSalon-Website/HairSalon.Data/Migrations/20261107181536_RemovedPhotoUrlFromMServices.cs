using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSalon.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPhotoUrlFromMServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicePhotoExampleUrl",
                table: "Services");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServicePhotoExampleUrl",
                table: "Services",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "ServicePhotoExampleUrl",
                value: "/images/services/mens-cut-and-beard.png");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "ServicePhotoExampleUrl",
                value: "/images/services/mens-haircut.png");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "ServicePhotoExampleUrl",
                value: "/images/services/beard-trim.png");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                column: "ServicePhotoExampleUrl",
                value: "/images/services/royal-shave.png");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 5,
                column: "ServicePhotoExampleUrl",
                value: "/images/services/kids-haircut.png");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 6,
                column: "ServicePhotoExampleUrl",
                value: "/images/services/hot-towel.png");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 7,
                column: "ServicePhotoExampleUrl",
                value: "/images/services/face-wax.png");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 8,
                column: "ServicePhotoExampleUrl",
                value: "/images/services/eyebrow-shaping.png");
        }
    }
}
