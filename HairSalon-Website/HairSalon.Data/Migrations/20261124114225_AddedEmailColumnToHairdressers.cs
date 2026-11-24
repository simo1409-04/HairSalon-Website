using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSalon.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmailColumnToHairdressers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "HairDressers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "HairDressers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "simeon.ivanov@gmail.com");

            migrationBuilder.UpdateData(
                table: "HairDressers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "viktoria.petrova@gmail.com");

            migrationBuilder.UpdateData(
                table: "HairDressers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Email",
                value: "martin.ivanov@gmail.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "HairDressers");
        }
    }
}
