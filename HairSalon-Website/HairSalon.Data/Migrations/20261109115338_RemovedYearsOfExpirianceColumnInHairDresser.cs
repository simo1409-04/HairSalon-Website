using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSalon.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedYearsOfExpirianceColumnInHairDresser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearsOfExperiance",
                table: "HairDressers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperiance",
                table: "HairDressers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "HairDressers",
                keyColumn: "Id",
                keyValue: 1,
                column: "YearsOfExperiance",
                value: 7);

            migrationBuilder.UpdateData(
                table: "HairDressers",
                keyColumn: "Id",
                keyValue: 2,
                column: "YearsOfExperiance",
                value: 9);

            migrationBuilder.UpdateData(
                table: "HairDressers",
                keyColumn: "Id",
                keyValue: 3,
                column: "YearsOfExperiance",
                value: 4);
        }
    }
}
