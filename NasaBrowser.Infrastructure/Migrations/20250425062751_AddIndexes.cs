using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NasaBrowser.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Asteroids_Name",
                table: "Asteroids",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Asteroids_RecClass",
                table: "Asteroids",
                column: "RecClass");

            migrationBuilder.CreateIndex(
                name: "IX_Asteroids_Year",
                table: "Asteroids",
                column: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Asteroids_Name",
                table: "Asteroids");

            migrationBuilder.DropIndex(
                name: "IX_Asteroids_RecClass",
                table: "Asteroids");

            migrationBuilder.DropIndex(
                name: "IX_Asteroids_Year",
                table: "Asteroids");
        }
    }
}
