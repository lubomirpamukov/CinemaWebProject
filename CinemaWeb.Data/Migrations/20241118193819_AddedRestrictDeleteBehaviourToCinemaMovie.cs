using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRestrictDeleteBehaviourToCinemaMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersMovies",
                table: "UsersMovies");

            migrationBuilder.DropIndex(
                name: "IX_UsersMovies_UserId",
                table: "UsersMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemasMovies",
                table: "CinemasMovies");

            migrationBuilder.DropIndex(
                name: "IX_CinemasMovies_CinemaId",
                table: "CinemasMovies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersMovies",
                table: "UsersMovies",
                columns: new[] { "UserId", "MovieId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemasMovies",
                table: "CinemasMovies",
                columns: new[] { "CinemaId", "MovieId" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersMovies_MovieId",
                table: "UsersMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_CinemasMovies_MovieId",
                table: "CinemasMovies",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersMovies",
                table: "UsersMovies");

            migrationBuilder.DropIndex(
                name: "IX_UsersMovies_MovieId",
                table: "UsersMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemasMovies",
                table: "CinemasMovies");

            migrationBuilder.DropIndex(
                name: "IX_CinemasMovies_MovieId",
                table: "CinemasMovies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersMovies",
                table: "UsersMovies",
                columns: new[] { "MovieId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemasMovies",
                table: "CinemasMovies",
                columns: new[] { "MovieId", "CinemaId" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersMovies_UserId",
                table: "UsersMovies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CinemasMovies_CinemaId",
                table: "CinemasMovies",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemasMovies_Movies_MovieId",
                table: "CinemasMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
