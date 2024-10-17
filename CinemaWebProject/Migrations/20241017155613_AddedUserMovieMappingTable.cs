using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaWebProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserMovieMappingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Cinemas_CinemaId",
                table: "CinemasMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies");

            migrationBuilder.CreateTable(
                name: "UsersMovies",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersMovies", x => new { x.MovieId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersMovies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersMovies_UserId",
                table: "UsersMovies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasMovies_Cinemas_CinemaId",
                table: "CinemasMovies",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Cinemas_CinemaId",
                table: "CinemasMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies");

            migrationBuilder.DropTable(
                name: "UsersMovies");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasMovies_Cinemas_CinemaId",
                table: "CinemasMovies",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
