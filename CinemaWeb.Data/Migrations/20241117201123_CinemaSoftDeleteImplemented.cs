using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class CinemaSoftDeleteImplemented : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cinemas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cinemas");
        }
    }
}
